using GAIA.Common.Classes;
using System.Collections.Generic;

namespace GAIA.CMS
{
    public static class Deploy
    {
        public static string TemplateMarker = "<#CMS_TEMPLATE#>";

        public static void Execute(BaseConfig baseConfig, ImagesConfig imagesConfig, Dictionary<string, object> fullConfig)
        {
            DeployHelper.CopyTemplateToTargetPath(baseConfig);

            DeployHelper.ExecuteTemplate(baseConfig, fullConfig, TemplateMarker);

            DeployHelper.ProcessImages(baseConfig, imagesConfig);
        }
    }
}
