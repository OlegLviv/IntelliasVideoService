using Newtonsoft.Json;

namespace IVS.Data.JSONParser
{
    public class Parser
    {
        public static JsonData ParseFileContent()
        {
            var jsontext = System.IO.File.ReadAllText(@"Data.json");
            var data = JsonConvert.DeserializeObject<JsonData>(jsontext);
            return data;
        }
    }
}
