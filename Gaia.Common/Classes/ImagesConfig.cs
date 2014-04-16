using System.Collections.Generic;

namespace Gaia.Common.Classes
{
    public class LogoImage
    {
        public string OutputName { get; set; }
        public string Width { get; set; }
        public string Height { get; set; }
    }

    public class ImagesConfig
    {
        public string LogoImage { get; set; }
        public string WaterMarkImage { get; set; }
        public string ImgFolder { get; set; }

        public List<LogoImage> LogoImages { get; set; }
    }
}
