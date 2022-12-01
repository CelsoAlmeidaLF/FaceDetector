using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Face;
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
        bool EnabledSaveImage = false;
        bool EnabledTrain = false;
        Image<Bgr, Byte> currentFrame;
        List<Image> bitmaps = new List<Image>();
        public Bitmap rCompativel;
        public Bitmap rInCompativel;
        private InteligentEmgu inteligent;
        public Image<Bgr, byte> resultImage { get; private set; }
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
            inteligent = new InteligentEmgu();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnTrain.Enabled = false;
            btnCapt.Enabled = false;
            EnabledTrain = true;
            TrainProcess(sender, e);
        }
        #endregion

        private void VideoCapture_ImageGrabbed(object? sender, EventArgs e)
        {
            videoCapture.Retrieve(frame, 0);
            currentFrame = frame.ToImage<Bgr, Byte>().Resize(imgPrincipal.Width, imgPrincipal.Height, Inter.Cubic);

            if (true)
            {
                Rectangle[] faces = inteligent.Initialize(currentFrame);
                if (faces.Length > 0)
                {
                    foreach (var face in faces)
                    {
                        resultImage = inteligent.Render(currentFrame, face);
                        imgResult.SizeMode = inteligent.Result.SizeMode;
                        imgResult.Image = inteligent.Result.Image;

                        if (EnabledSaveImage) SaveImage(@"\TrainedImages");
                        if (EnabledTrain)
                        {
                            if ((imgResult.Image != null && picResultTrain.Image != null))
                            {
                                inteligent.compativel = inteligent.ComparaImagem(resultImage.Resize(200, 200, Inter.Cubic).AsBitmap(), picResultTrain.Image);
                                BuildInteligentImage(sender, e);
                                if (inteligent.FuzzyISO("basic"))
                                {
                                    if (inteligent.FuzzyISO("complet"))
                                    {
                                        RectangleFaceDetect("Cath", face);
                                        SaveImage(@"\TrainedImages\Out\");
                                    }
                                    else
                                    {
                                        RectangleFaceDetect("Ativo", face);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            imgPrincipal.Image = currentFrame.AsBitmap();
        }

        private void RectangleFaceDetect(string label, Rectangle face)
        {
            CvInvoke.PutText(currentFrame, label, new Point(face.X - 2, face.Y - 2),
                FontFace.HersheyComplex, 0.9, new Bgr(Color.Orange).MCvScalar);
            CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);
        }

        private void SaveImage(string vFolder)
        {
            string path = Directory.GetCurrentDirectory() + vFolder;
            if (!Directory.Exists(path)) 
                Directory.CreateDirectory(path);

            Task.Factory.StartNew(() => {
                for (int i = 0; i < 6; i++)
                {
                    resultImage.Resize(200, 200, Inter.Cubic).Save(path + @"\" +
                        Environment.UserName + "_" +
                        DateTime.Now.ToString("ddmmyyyyhhmmss") + ".jpg");
                    Thread.Sleep(1000);
                }
            });

            EnabledSaveImage = false;
        }

        private void BuildInteligentImage(object? sender, EventArgs e)
        {
            this.imgCompativel.Image = inteligent.ImageCompativel;
            this.imgIncompativel.Image = inteligent.ImageInCompativel;
            this.tssCompativel.Text = inteligent.TextCompativel;
            this.tssIncompativel.Text = inteligent.TextIncompativel;
            this.tssTaxaPixel.Text = inteligent.TextTaxaPixel;
        }

        private void TrainProcess(object sender, EventArgs e)
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

    }
}