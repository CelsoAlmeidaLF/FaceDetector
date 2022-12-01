using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using MyFace.Util;
using System.Diagnostics;

namespace MyFace
{
    public partial class frmMyFace : Form
    {
        #region 'Propriedades da classe'
        double Max = 0;
        public ImageFile _image;
        VideoCapture videoCapture;
        Mat frame = new Mat();
        CascadeClassifier cascadeClassifier = new CascadeClassifier("haarcascade_frontalface_alt.xml");
        bool EnabledSaveImage = false;
        bool EnabledTrain = false;
        Image<Bgr, Byte> currentFrame;
        List<Image> bitmaps = new List<Image>();
        public bool[] compativel { get; set; }
        public Bitmap rCompativel { get; set; }
        public Bitmap rInCompativel { get; set; }
        Dictionary<string, double[]> isos;
        #endregion

        public frmMyFace()
        {
            InitializeComponent();
        }

        #region 'methods events'
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

        private void limparToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + @"\TrainedImages";
            string[] files = Directory.GetFiles(path);

            if (files.Length > 0)
                Directory.Delete(path, true);

            btnCapt.Enabled = true;
            EnabledSaveImage = false;
            EnabledTrain = false;
        }

        private void frmMyFace_Load(object sender, EventArgs e)
        {
            isos = new Dictionary<string, double[]>();
            isos.Add("basic", new double[] { 0, 0, 0, 0, 0 });
            isos.Add("complet", new double[] { 0, 0, 0, 0, 0 });

        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnTrain.Enabled = false;
            btnCapt.Enabled = false;
            EnabledTrain = true;
            TrainProcess();
        }
        #endregion

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
                                compativel = ComparaImagem(resultImage.Resize(200, 200, Inter.Cubic).AsBitmap(), picResultTrain.Image);

                                if (FuzzyISO("basic"))
                                {
                                    if (FuzzyISO("complet"))
                                    {
                                        string path = Directory.GetCurrentDirectory() + @"\TrainedImages\Out\";

                                        CvInvoke.PutText(currentFrame, "Cath", new Point(face.X - 2, face.Y - 2),
                                            FontFace.HersheyComplex, 0.9, new Bgr(Color.Orange).MCvScalar);
                                        CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);

                                        if (!Directory.Exists(path))
                                            Directory.CreateDirectory(path);

                                        Task.Factory.StartNew(() =>
                                        {
                                            for (int i = 0; i < 3; i++)
                                            {
                                                resultImage.Resize(200, 200, Inter.Cubic).Save(path + @"\" +
                                                    Environment.UserName + "_out_" +
                                                    DateTime.Now.ToString("ddmmyyyyhhmmss") + ".jpg");
                                                Thread.Sleep(1000);
                                            }
                                        });
                                    }
                                    else
                                    {
                                        CvInvoke.PutText(currentFrame, "Ativo", new Point(face.X - 2, face.Y - 2),
                                            FontFace.HersheyComplex, 0.9, new Bgr(Color.Orange).MCvScalar);
                                        CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);
                                    }

                                }
                                
                            }
                        }
                    }
                }
            }

            imgPrincipal.Image = currentFrame.AsBitmap();
        }

        private bool FuzzyISO(string iso)
        {
            double[] arg = isos[iso];

            if (compativel == null) return false;
            else
            {
                if (arg[0] != 0 && arg[1] == 0 && arg[2] == 0 && arg[3] == 0 && arg[4] == 0)
                    return arg[0] < compativel.Length ? true : false;

                if (arg[0] != 0 && arg[1] != 0 && arg[2] != 0 && arg[3] == 0 && arg[4] == 0)
                    return (arg[0] < compativel.Length && arg[1] > compativel.Length) ||
                        arg[2] < compativel.Length ? true : false;

                if (arg[0] != 0 && arg[1] != 0 && arg[2] != 0 && arg[3] != 0 && arg[4] != 0)
                    return (arg[0] < compativel.Length && arg[1] > compativel.Length) ||
                        (arg[2] < compativel.Length && arg[3] > compativel.Length) ||
                        arg[4] < compativel.Length ? true : false;

                else return false;
            }
        }

        private bool FuzzyImage(int[] arg)
        {
            if (compativel == null) return false;
            else
            {
                if (arg[0] != 0 && arg[1] == 0 && arg[2] == 0 && arg[3] == 0 && arg[4] == 0)
                    return arg[0] < compativel.Length ? true : false;

                if (arg[0] != 0 && arg[1] != 0 && arg[2] != 0 && arg[3] == 0 && arg[4] == 0)
                    return (arg[0] < compativel.Length && arg[1] > compativel.Length) ||
                        arg[2] < compativel.Length ? true : false;

                if (arg[0] != 0 && arg[1] != 0 && arg[2] != 0 && arg[3] != 0 && arg[4] != 0)
                    return (arg[0] < compativel.Length && arg[1] > compativel.Length) ||
                        (arg[2] < compativel.Length && arg[3] > compativel.Length) ||
                        arg[4] < compativel.Length ? true : false;

                else return false;
            }
        }

        private bool[] ComparaImagem(Image image1, Image image2)
        {
            List<bool> compativel = new List<bool>();
            List<bool> incompativel = new List<bool>();

            Bitmap? map1 = CreateBitmap(image1);
            Bitmap? map2 = CreateBitmap(image2);

            if (map1 != null && map2 != null)
            {
                rCompativel = new Bitmap(map1.Width, map1.Height);
                rInCompativel = new Bitmap(map1.Width, map1.Height);

                    for (int x = 0; x < map1.Width; x++)
                    {
                        for (int y = 0; y < map1.Height; y++)
                        {
                            if (ValidatedImagePixel(map1, map2, x, y))
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

            if (FuzzyImage(new int[] { 300, 900, 2000, 0, 0 }))
            {
#if DEBUG
                Debug.WriteLine("DataLog: " + DateTime.Now);
                Debug.WriteLine("compativel: " + porcCompativel.ToString("0.00") + "%");
                Debug.WriteLine("compativel: " + compativel.Count + "; incompativel: " + incompativel.Count);
#endif
                if (Max < compativel.Count)
                {
                    if (Max < compativel.Count) Max = compativel.Count;
                    isos["basic"] = new double[] { Max - (Max / 2), Max + (Max * 2), Max - (Max / 3), Max + (Max / 2), Max };
                    isos["complet"] = new double[] { 1300, 0, 0, 0, 0 };
                    this.tssCompativel.Text = compativel.Count.ToString();
                }
                
                this.tssIncompativel.Text = incompativel.Count.ToString();
                this.tssTaxaPixel.Text = porcCompativel.ToString("0.00") + "%";
            }
            
            return compativel.ToArray();
        }

        private Bitmap CreateBitmap(Image image)
        {
            Bitmap bitmap = new Bitmap(image);
            Image item = EmguConvertImage(image, ref bitmap);
            return item != null ? new Bitmap(item, item.Width, item.Height) : new Bitmap(200, 200);
        }

        private bool ValidatedImagePixel(Bitmap map1, Bitmap map2, int x, int y)
        {
            return map1.GetPixel(x, y).Equals(map2.GetPixel(x, y)) ? true : false;
        }

        private Image EmguConvertImage(Image image, ref Bitmap bitmap)
        {
            Mat mat = bitmap.ToMat();
            Image<Gray, Byte> Image = mat.ToImage<Gray, byte>();
            Image<Gray, Byte> img = Image.Convert<Gray, Byte>().Resize(200, 200, Inter.Cubic);
            CvInvoke.EqualizeHist(img, img);
            return img.AsBitmap();
        }

        private void TrainProcess()
        {
            string path;
            if (txtCaminho.Text != "") path = Directory.GetCurrentDirectory() + @"\TrainedImages" + "\\" + txtCaminho;
            else path = Directory.GetCurrentDirectory() + @"\TrainedImages";
            string[] files = Directory.GetFiles(path);

            // Cria Coleção de Imagens
            CreateColectionImage(files);

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

                if (bitBeta == null) { bitBeta = bitAlfa; }
            }

            picResultTrain.Image = result;
        }

        private void CreateColectionImage(string[] files)
        {
            foreach (string file in files)
            {
                Debug.WriteLine(file);
                Image<Gray, byte> trainedImage = new Image<Gray, byte>(file).Resize(200, 200, Inter.Cubic);
                CvInvoke.EqualizeHist(trainedImage, trainedImage);
                bitmaps.Add(trainedImage.AsBitmap());
            }
        }

        #region 'notImplement'
#if v1_0_0_0
        private bool ValidatedSizeImages(Image item1, Image item2)
        {
            return item1.Width.Equals(item2.Width) && item1.Height.Equals(item2.Height) ? true : false;
        }
#endif
        #endregion

    }
}