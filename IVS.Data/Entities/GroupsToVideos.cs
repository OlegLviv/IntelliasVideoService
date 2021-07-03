using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IVS.Data.Entities
{
    public class GroupsToVideos
    {
        public int GroupId { get; set; }

        public Group Group { get; set; }

        public int VideoId { get; set; }

        public Video Video { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Priority Priority { get; set; }
    }
}
