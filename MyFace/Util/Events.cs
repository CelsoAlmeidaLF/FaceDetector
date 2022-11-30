namespace MyFace.Util
{
    public class EventsImage
    {
        public static ImageFile vImageFile { get; set; }

        public static ImageFile OpenFileDialog(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                string caminho = open.FileName;
                Image imgDoc = new Bitmap(caminho);

                vImageFile = new ImageFile()
                {
                    Image = imgDoc,
                    Caminho = caminho
                };
            }
            return vImageFile;
        }
    }

    public class ImageFile
    {
        public Image Image { get; set; }
        public string Caminho { get; set; } = String.Empty;
    }

}
