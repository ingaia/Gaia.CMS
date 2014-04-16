using System.Web.Script.Serialization;

namespace Gaia.Common
{
    public static class JsonHelper
    {
        public static T Deserialize<T>(string jsonObject)
        {
            var serializer = new JavaScriptSerializer();
            return serializer.Deserialize<T>(jsonObject);
        }
    }
}
