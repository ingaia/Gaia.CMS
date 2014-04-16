using System.Configuration;

namespace Gaia.Common
{
    public class ConfigReader
    {
        public static string Read(string key)
        {
            return ConfigurationManager.AppSettings[key] != null ? ConfigurationManager.AppSettings[key] : "";
        }
    }
}
