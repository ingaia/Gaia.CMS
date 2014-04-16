using GAIA.Common;
using GAIA.Common.Classes;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace GAIA.CMS
{
    internal static class DeployHelper
    {
        public static void CopyTemplateToTargetPath(BaseConfig config)
        {
            var baseTargetPath = Path.Combine(config.TargetPath, config.ClientDirName);
            var modelTargetPath = Path.Combine(baseTargetPath, config.ModelFolder);

            IOHelper.CopyAll(Path.Combine(config.SourcePath, config.DefaultTemplateFolder), baseTargetPath);

            IOHelper.CopyAll(Path.Combine(config.SourcePath, config.TemplateName), modelTargetPath);

            IOHelper.Move(Path.Combine(modelTargetPath, "index.aspx"), Path.Combine(baseTargetPath, "index.aspx"));
        }

        public static void ExecuteTemplate(BaseConfig config, Dictionary<string, object> fullConfig, string marker)
        {
            var sFiles = IOHelper.GetTemplateFiles(config.TargetPath, marker).ToList();

            foreach (var file in sFiles)
            {
                var content = File.ReadAllText(file, Encoding.GetEncoding(1252));

                var result = Nustache.Core.Render.StringToString(content, (object)fullConfig);

                File.WriteAllText(file, result, Encoding.GetEncoding(1252));
            }
        }

        public static void ProcessImages(BaseConfig config, ImagesConfig imagesConfig)
        {
            var imagePath = Path.Combine(config.TargetPath, config.ClientDirName, config.ModelFolder, imagesConfig.ImgFolder);

            if (!imagesConfig.LogoImage.IsNullOrEmpty() && File.Exists(imagesConfig.LogoImage))
            {
                var imgLogo = imagesConfig.LogoImage;
                var imgLogoFile = Path.GetFileName(imgLogo);
                IOHelper.Copy(imgLogo, Path.Combine(imagePath, imgLogoFile), true);

                foreach (var img in imagesConfig.LogoImages)
                {
                    ImageHelper.ResizeToFile(imgLogo, img.Width.ToInt(), img.Height.ToInt(), Path.Combine(imagePath, img.OutputName));
                }
            }

            if (imagesConfig.WaterMarkImage.IsNullOrEmpty() || !File.Exists(imagesConfig.LogoImage)) return;

            var watermark = imagesConfig.WaterMarkImage;
            var imgWaterMarkFile = Path.GetFileName(watermark);
            IOHelper.Copy(watermark, Path.Combine(imagePath, imgWaterMarkFile), true);
        }
    }
}
