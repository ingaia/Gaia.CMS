using System.Drawing;
using System.Drawing.Drawing2D;

namespace Gaia.Common
{
    public static class ImageHelper
    {
        public static void ResizeToFile(string srcImagePath, int newWidth, int newHeight, string destinationFile)
        {
            var newImageSize = new Size(newWidth, newHeight);

            Bitmap newImage = new Bitmap(newImageSize.Width, newImageSize.Height);

            var srcImage = Image.FromFile(srcImagePath);

            using (Graphics gr = Graphics.FromImage(newImage))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;
                gr.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gr.PixelOffsetMode = PixelOffsetMode.HighQuality;
                gr.DrawImage(srcImage, new Rectangle(0, 0, newImageSize.Width, newImageSize.Height));
            }

            SaveImage(newImage, destinationFile);
        }

        public static void SaveImage(Bitmap image, string path)
        {
            image.Save(path);
        }
    }
}
