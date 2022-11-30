using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Reg;
using Emgu.CV.Structure;
using MyFace.Util;
using System;
using System.Diagnostics;
using System.IO;

namespace MyFace
{
    public partial class frmMyFace : Form
    {
        #region 'Propriedades da classe'
        int Max = 0;
        public ImageFile _image;
        VideoCapture videoCapture;
        Mat frame = new Mat();
        CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt.xml");
        bool EnabledSaveImage = false;
        bool EnabledTrain = false;
        Image<Bgr, Byte> currentFrame;
        List<Image> bitmaps = new List<Image>();
        public bool[] compativel { get; set; }
        public Bitmap rCompativel { get; private set; }
        public Bitmap rInCompativel { get; private set; }
        #endregion

        public frmMyFace()
        {
            InitializeComponent();
        }

        private void txtCaminho_Click(object sender, EventArgs e)
        {
            _image = EventsImage.OpenFileDialog(sender, e);
            if (_image.Image != null)
            {
                this.imgPrincipal.Image = _image.Image;
                this.txtCaminho.Text = _image.Caminho;
            }
        }

        private void btnCapt_Click(object sender, EventArgs e)
        {
            EnabledSaveImage = true;
            btnTrain.Enabled = true;
        }

        private void btnWebCam_Click(object sender, EventArgs e)
        {
            if (videoCapture != null && videoCapture.IsOpened) return; 
            videoCapture = new VideoCapture();      
            videoCapture.ImageGrabbed += VideoCapture_ImageGrabbed;
            videoCapture.Start();
        }

        private void VideoCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            videoCapture.Retrieve(frame, 0);
            currentFrame = frame.ToImage<Bgr, Byte>().Resize(imgPrincipal.Width, imgPrincipal.Height, Inter.Cubic);

            if (true)
            {
                Mat grayImage = new Mat();
                CvInvoke.CvtColor(currentFrame, grayImage, ColorConversion.Bgr2Gray);
                CvInvoke.EqualizeHist(grayImage, grayImage);
                Rectangle[] faces = cascadeClassifier.DetectMultiScale(grayImage, 1.2, 3);

                if (faces.Length > 0)
                {
                    foreach (var face in faces)
                    {
                        CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Yellow).MCvScalar, 2);
                        Image<Bgr, Byte> resultImage = currentFrame.Convert<Bgr, Byte>();
                        resultImage.ROI = face;
                        imgResult.SizeMode = PictureBoxSizeMode.StretchImage;
                        imgResult.Image = resultImage.Resize(200, 200, Inter.Cubic).AsBitmap();

                        if (EnabledSaveImage)
                        {
                            string path = Directory.GetCurrentDirectory() + @"\TrainedImages";

                            if (!Directory.Exists(path))
                                Directory.CreateDirectory(path);

                            Task.Factory.StartNew(() => {
                                for (int i = 0; i < 6; i++)
                                {
                                    resultImage.Resize(200, 200, Inter.Cubic).Save(path + @"\" +
                                        Environment.UserName  + "_" +
                                        DateTime.Now.ToString("ddmmyyyyhhmmss") + ".jpg");
                                    Thread.Sleep(1000);
                                }
                            });

                            EnabledSaveImage = false;
                        }

                        if (EnabledTrain)
                        {
                            if ((imgResult.Image != null && picResultTrain.Image != null))
                            {
                                compativel = ComparaImagem((Image)resultImage.Resize(200, 200, Inter.Cubic).AsBitmap(), (Image)picResultTrain.Image);

                                if(FuzzyImage(1000))
                                {
                                    CvInvoke.PutText(currentFrame, "Ativo", new Point(face.X - 2, face.Y - 2),
                                        FontFace.HersheyComplex, 0.9, new Bgr(Color.Orange).MCvScalar);
                                    CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);

                                    if (FuzzyImage(1500))
                                    {
                                        string path = Directory.GetCurrentDirectory() + @"\TrainedImages\Out\";

                                        if (!Directory.Exists(path))
                                            Directory.CreateDirectory(path);

                                        Task.Factory.StartNew(() => {
                                            for (int i = 0; i < 6; i++)
                                            {
                                                resultImage.Resize(200, 200, Inter.Cubic).Save(path + @"\" +
                                                    Environment.UserName + "_out_" +
                                                    DateTime.Now.ToString("ddmmyyyyhhmmss") + ".jpg");
                                                Thread.Sleep(1000);
                                            }
                                        });
                                    }                                      
                                }
                            }
                        }
                    }
                }
            }

            imgPrincipal.Image = currentFrame.AsBitmap();
        }

        private bool FuzzyImage(int min, int? max = null)
        {
            if (compativel == null) return false;
            else
            {
                if (max != null) return compativel.Length > min && compativel.Length < max ? true : false;
                else return compativel.Length > min ? true : false;
            }
        }

        private bool[] ComparaImagem(Image image1, Image image2)
        {
            List<bool> compativel = new List<bool>();
            List<bool> incompativel = new List<bool>();

            Bitmap? map1 = new Bitmap(image1);
            Image item1 = EmguConvertImage(image1, ref map1);
            Bitmap? map2 = new Bitmap(image2);
            Image item2 = EmguConvertImage(image2, ref map2);

            if (ValidatedSizeImages(item1, item2)) // item1.Width.Equals(item2.Width) && item1.Height.Equals(item2.Height))
            {
                map1 = new Bitmap(item1, item1.Width, item1.Height);
                map2 = new Bitmap(item2, item2.Width, item2.Height);
            }
            else
            {
                return new bool[1]{ false };
            }

            if (map1 != null && map2 != null)
            {
                rCompativel = new Bitmap(map1.Width, map1.Height);
                rInCompativel = new Bitmap(map2.Width, map2.Height);

                for (int x = 0; x < map1.Width; x++)
                {
                    for(int y = 0; y < map1.Height; y++)
                    {
                        if (ValidatedImagePixel(map1, map2, x, y)) // map1.GetPixel(x, y).Equals(map2.GetPixel(x, y)))
                        {
                            rCompativel.SetPixel(x, y, map1.GetPixel(x, y));
                            compativel.Add(true);
                        }
                        else
                        {
                            rInCompativel.SetPixel(x, y, map1.GetPixel(x, y));
                            incompativel.Add(true);
                        }
                    }
                }

                imgCompativel.Image = rCompativel;
                imgIncompativel.Image = rInCompativel;
            }

            double total = compativel.Count + incompativel.Count;
            double porcCompativel = (compativel.Count/total) *100;

            if ((compativel.Count > 964 && compativel.Count < 999) || compativel.Count > 1232)
            {
#if DEBUG
                Debug.WriteLine("DataLog: " + DateTime.Now);
                Debug.WriteLine("compativel: " + porcCompativel.ToString("0.00") + "%");
                Debug.WriteLine("compativel: " + compativel.Count + "; incompativel: " + incompativel.Count);
#endif
                if (Max < compativel.Count)
                {
                    if (Max < compativel.Count) Max = compativel.Count;
                    this.tssCompativel.Text = compativel.Count.ToString();
                }
                this.tssIncompativel.Text = incompativel.Count.ToString();
                this.tssTaxaPixel.Text = porcCompativel.ToString("0.00") + "%";
            }
            
            return compativel.ToArray();
        }

        private bool ValidatedImagePixel(Bitmap map1, Bitmap map2, int x, int y)
        {
            return map1.GetPixel(x, y).Equals(map2.GetPixel(x, y)) ? true : false;
        }

        private bool ValidatedSizeImages(Image item1, Image item2)
        {
            return item1.Width.Equals(item2.Width) && item1.Height.Equals(item2.Height) ? true : false;
        }

        private Image EmguConvertImage(Image image, ref Bitmap bitmap)
        {
            Mat mat = bitmap.ToMat();
            Image<Gray, Byte> Image = mat.ToImage<Gray, byte>();
            Image<Gray, Byte> img = Image.Convert<Gray, Byte>().Resize(200, 200, Inter.Cubic);
            CvInvoke.EqualizeHist(img, img);
            return img.AsBitmap();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnTrain.Enabled = false;
            btnCapt.Enabled = false;
            EnabledTrain = true;
            TrainProcess();
        }

        private void TrainProcess()
        {
            string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
            string[] files = Directory.GetFiles(path);

            foreach (string file in files)
            {
                Debug.WriteLine(file);           
                Image<Gray, byte> trainedImage = new Image<Gray, byte>(file).Resize(200, 200, Inter.Cubic);
                CvInvoke.EqualizeHist(trainedImage, trainedImage);
                bitmaps.Add(trainedImage.AsBitmap());
            }

            Debug.WriteLine("PROCESS IMAGE ...");

            Bitmap? bitBeta = null;
            Bitmap result = new Bitmap(32, 32);

            foreach (Image image in bitmaps)
            {
                Bitmap bitAlfa = new Bitmap(image);
                
                result = new Bitmap(bitAlfa.Width, bitAlfa.Height);

                for (int x = 0; x < bitAlfa.Width; x++)
                {
                    for (int y = 0; y < bitAlfa.Height; y++)
                    {
                        if (bitBeta != null && bitAlfa.GetPixel(x, y).Equals(bitBeta.GetPixel(x, y)))
                        {
                            result.SetPixel(x, y, bitBeta.GetPixel(x, y));
                        }
                    }
                }

                if (bitBeta == null)
                {
                    bitBeta = bitAlfa;
                }
            }

            picResultTrain.Image = result;
        }

        private void limparToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
            string[] files = Directory.GetFiles(path);

            if(files.Length > 0) 
                Directory.Delete(path, true);

            btnCapt.Enabled = true;
            EnabledSaveImage = false;
            EnabledTrain= false;
        }
    }
}