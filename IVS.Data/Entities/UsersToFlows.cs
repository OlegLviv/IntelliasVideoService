using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IVS.Data.Entities
{
    public class UsersToFlows
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int FlowId  { get; set; }

        public Flow Flow { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Priority Priority { get; set; }
    }
}
