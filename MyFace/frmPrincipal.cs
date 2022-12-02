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
        bool EnabledSaveImage = false;
        bool EnabledTrain = false;
        Image<Bgr, Byte> currentFrame;
        List<Image> bitmaps = new List<Image>();
        public Bitmap rCompativel;
        public Bitmap rInCompativel;
        private InteligentEmgu inteligent;
        public Image<Bgr, byte> resultImage { get; private set; }
        public string ISO { get; private set; }
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
            ISO = cbISOS.Text;
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

                        if (EnabledSaveImage) SaveImage(@"\");
                        if (EnabledTrain)
                        {
                            if ((imgResult.Image != null && picResultTrain.Image != null))
                            {
                                inteligent.compativel = inteligent.ComparaImagem(
                                    ISO,
                                    resultImage.Resize(200, 200, Inter.Cubic).AsBitmap(), 
                                    picResultTrain.Image);
                                BuildInteligentImage(sender, e);
                                if (inteligent.FuzzyISO("basic"))
                                {
                                    if (inteligent.FuzzyISO("complet"))
                                    {
                                        RectangleFaceDetect("Cath", face);
                                        SaveImage(@"\Saida\");
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
        #endregion

        private void RectangleFaceDetect(string label, Rectangle face)
        {
            CvInvoke.PutText(currentFrame, label, new Point(face.X - 2, face.Y - 2),
                FontFace.HersheyComplex, 0.9, new Bgr(Color.Orange).MCvScalar);
            CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Green).MCvScalar, 2);
        }
        private void SaveImage(string folder)
        {
            try
            {
                string path = Directory.GetCurrentDirectory() + @"\TrainedImages" + folder;
                if (!Directory.Exists(path)) Directory.CreateDirectory(path);

                Task.Factory.StartNew(() =>
                {
                    for (int i = 0; i < 6; i++)
                    {
                        resultImage.Resize(200, 200, Inter.Cubic).Save(path +
                            Environment.UserName + "_" +
                            DateTime.Now.ToString("ddmmyyyyhhmmss") + ".jpg");
                        Thread.Sleep(1000);
                    }
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Erro ao salvar", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                EnabledSaveImage = false;
            }
        }
        private void BuildInteligentImage(object? sender, EventArgs e)
        {
            this.imgCompativel.Image = inteligent.ImageCompativel;
            this.imgIncompativel.Image = inteligent.ImageInCompativel;
            this.tssCompativel.Text = inteligent.TextCompativel + " / " + inteligent.compativel.Length;
            this.tssIncompativel.Text = inteligent.TextIncompativel;
            this.tssTaxaPixel.Text = inteligent.TextTaxaPixel;
        }
        private void TrainProcess(object sender, EventArgs e)
        {
            string path;
            int arg = 6;
            int R = arg; 

            if ((string)cbISOS.Text != "") path = Directory.GetCurrentDirectory() + @"\TrainedImages" + "\\" + cbISOS.Text;
            else path = Directory.GetCurrentDirectory() + @"\TrainedImages";
            string[] files = Directory.GetFiles(path);
            string[] xfiles = new string[4];

            if(files != null || files.Length != 0)
                for (int i = 0; i < 4; i++)
                    xfiles[i] = files[i];

            CreateColectionImage(xfiles);

            Bitmap[] images = new Bitmap[2];
            Bitmap result = new Bitmap(200, 200);

            foreach (Image image in bitmaps)
            {
                images[0] = new Bitmap(image);      
                result = new Bitmap(image.Width, image.Height);
                if (images[0] != null && images[1] != null)
                {
                    for (int y = 0; y < images[0].Height; y++)
                    {
                        for (int x = 0; x < images[0].Width; x++)
                        {
                            Color[] color = new Color[2];
                            color[0] = images[0].GetPixel(x, y);
                            color[1] = images[1].GetPixel(x, y);

                            if (color[0].R >= color[1].R - R && color[0].R <= color[1].R + R)
                                result.SetPixel(x, y, images[0].GetPixel(x, y));
                        }
                    }
                }             
                images[1] = images[0];
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