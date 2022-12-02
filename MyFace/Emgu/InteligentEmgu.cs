using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using System.Diagnostics;

namespace MyFace
{
    public class InteligentEmgu
    {
        #region "Propriedades"
        public Dictionary<string, double[]> isos { get; set; }
        public Result Result { get; set; }
        public bool[] compativel { get; set; }
        public double compatibilidade { get; set; }
        public Bitmap ImageCompativel { get; private set; }
        public Bitmap ImageInCompativel { get; private set; }
        public int Max { get; private set; } = 12000;
        public Dictionary<string, int> ISO { get; private set; }
        public string TextCompativel { get; private set; }
        public string TextIncompativel { get; private set; }
        public string TextTaxaPixel { get; private set; }
        public CascadeClassifier cascadeClassifier { get; set; } = new CascadeClassifier("haarcascade_frontalface_alt.xml");
        #endregion

        public InteligentEmgu()
        {
            isos = new Dictionary<string, double[]>();
            isos.Add("basic", new double[] { 0, 0, 0, 0, 0 });
            isos.Add("complet", new double[] { 0, 0, 0, 0, 0 });

            ISO = new Dictionary<string, int>();
            ISO.Add("ISO100", 12000);
            ISO.Add("ISO150", 12000);
            ISO.Add("ISO200", 17000);
            ISO.Add("ISO300", 12000);
            ISO.Add("ISO400", 12000);
            ISO.Add("ISO500", 12000);
            ISO.Add("ISO600", 12000);
            ISO.Add("ISO700", 12000);
            ISO.Add("ISO800", 12000);
            ISO.Add("ISO900", 12000);
        }

        public bool FuzzyISO(string iso)
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

        public bool[] ComparaImagem(string iso, Image entrada, Image comparer)
        {
            List<bool> compativel = new List<bool>();
            List<bool> incompativel = new List<bool>();

            Bitmap? mapIn = CreateBitmap(entrada);
            Bitmap? mapOut = CreateBitmap(comparer);

            if (mapIn != null && mapOut != null)
            {
                ImageCompativel = new Bitmap(mapIn.Width, mapIn.Height);
                ImageInCompativel = new Bitmap(mapIn.Width, mapIn.Height);

                for (int x = 0; x < mapIn.Width; x++)
                {
                    for (int y = 0; y < mapIn.Height; y++)
                    {
                        if (ValidatedImagePixel(mapIn, mapOut, x, y))
                        {
                            ImageCompativel.SetPixel(x, y, mapIn.GetPixel(x, y));
                            compativel.Add(true);
                        }
                        else
                        {
                            ImageInCompativel.SetPixel(x, y, mapIn.GetPixel(x, y));
                            incompativel.Add(true);
                        }
                    }
                }
            }

            double total = compativel.Count + incompativel.Count;
            double porcCompativel = (compativel.Count / total) * 100;

            if (FuzzyImage(new int[] { 600, 1000, 5000, 0, 0 }))
            {
#if DEBUG
                Debug.WriteLine("DataLog: " + DateTime.Now);
                Debug.WriteLine("compativel: " + porcCompativel.ToString("0.00") + "%");
                Debug.WriteLine("compativel: " + compativel.Count + "; incompativel: " + incompativel.Count);
#endif
                if (Max < compativel.Count)
                {
                    if (Max < compativel.Count) Max = compativel.Count;
                    isos["basic"] = new double[] { Max - (Max / 4), Max, Max + 100 };
                    isos["complet"] = new double[] { ISO[iso], 0, 0, 0, 0 };
                    TextCompativel = compativel.Count.ToString();
                }

                TextIncompativel = incompativel.Count.ToString();
                TextTaxaPixel = porcCompativel.ToString("0.00") + "%";
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
            int arg = 20;
            int B = arg;
            Color[] color = new Color[2];
            color[0] = map1.GetPixel(x, y);
            color[1] = map2.GetPixel(x, y);
            if ((color[0].B >= color[1].B - B && color[0].B <= color[1].B + B)) return true;
            else return false;
        }

        private Image EmguConvertImage(Image image, ref Bitmap bitmap)
        {
            Mat mat = bitmap.ToMat();
            Image<Gray, Byte> Image = mat.ToImage<Gray, byte>();
            Image<Gray, Byte> img = Image.Convert<Gray, Byte>().Resize(200, 200, Inter.Cubic);
            CvInvoke.EqualizeHist(img, img);
            return img.AsBitmap();
        }

        public Rectangle[] Initialize(Image<Bgr, byte> currentFrame)
        {
            Mat grayImage = new Mat();
            CvInvoke.CvtColor(currentFrame, grayImage, ColorConversion.Bgr2Gray);
            CvInvoke.EqualizeHist(grayImage, grayImage);
            Rectangle[] faces = cascadeClassifier.DetectMultiScale(grayImage, 1.2, 3);
            return faces;
        }

        public Image<Bgr, byte> Render(Image<Bgr, byte> currentFrame, Rectangle face)
        {
            Result = new Result();
            CvInvoke.Rectangle(currentFrame, face, new Bgr(Color.Yellow).MCvScalar, 2);
            Image<Bgr, Byte> resultImage = currentFrame.Convert<Bgr, Byte>();
            resultImage.ROI = face;
            Result.SizeMode = PictureBoxSizeMode.StretchImage;
            Result.Image = resultImage.Resize(200, 200, Inter.Cubic).AsBitmap();
            return resultImage;
        }
    }
}