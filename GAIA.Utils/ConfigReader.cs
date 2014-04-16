using System.Configuration;

namespace GAIA.Common
{
    public class ConfigReader
    {
        public static string Read(string key)
        {
            return ConfigurationManager.AppSettings[key] != null ? ConfigurationManager.AppSettings[key] : "";
        }
    }
}
