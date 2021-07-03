using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IVS.Data.Entities
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Priority
    {
        Low,
        Medium,
        High,
        Critical
    }
}
