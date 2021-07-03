using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IVS.Data.Entities
{
    public class GroupsToFlows
    {
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public int FlowId { get; set; }

        public Flow Flow { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Priority Priority { get; set; }
    }
}
