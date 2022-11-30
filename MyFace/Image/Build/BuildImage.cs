using System.Runtime.Versioning;
using System;

namespace MyFace
{
    public class BuildImage
    {
        private Rectangle _drop { get; set; }
        private Image _image { get; set; }
        private Bitmap _map { get; set; }
        private Graphics _grap { get; set; }

        public BuildImage(Image image)
        {
            _image = image;
            _map = new Bitmap(image);
            _grap = Graphics.FromImage(_map);
            //DictionaryDropImage = new Dictionary<string, Rectangle>();
        }

        public Image DropImage()
        {
            _drop = new Rectangle(850, 318, 402, 432);
            _grap.DrawRectangle(new Pen(Color.Yellow, 8), _drop);
            return _map;
        }

        [SupportedOSPlatform("windows")]
        public Bitmap DropImage(Image img, Rectangle? drop = null) 
        {
            Bitmap target = new Bitmap(100, 100);

            if (img != null)
            {
                _drop = drop != null ? (Rectangle)drop : new Rectangle(850, 318, 402, 432);
                target = new Bitmap(_drop.Width, _drop.Height);
                Rectangle dest = new Rectangle(0, 0, _drop.Width, _drop.Height);
                Rectangle src = _drop;

                using (Graphics g = Graphics.FromImage(target))
                    g.DrawImage(img, dest, src, GraphicsUnit.Pixel);
            }

            return target;
        }
    }
}