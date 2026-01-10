using Newtonsoft.Json.Linq;
using System;
using System.IO;
using qa_dotnet_cucumber.DataModels;

namespace qa_dotnet_cucumber.Utilities 
{
    public class JsonHelper
    {
        private static string GetJsonPath()
        {
           
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "testdata.json");
        }

        public static EducationModel ReadEducationData(string key)
        {
            var json = File.ReadAllText(GetJsonPath());
            var jObject = JObject.Parse(json);
            return jObject[key].ToObject<EducationModel>();
        }

        public static CertificationModel ReadCertificationData(string key)
        {
            var json = File.ReadAllText(GetJsonPath());
            var jObject = JObject.Parse(json);
            return jObject[key].ToObject<CertificationModel>();
        }
    }
}