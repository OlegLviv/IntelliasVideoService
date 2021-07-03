using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace IVS.Data.Entities
{
    public class UsersToVideos
    {
        public int UserId { get; set; }

        public User User { get; set; }

        public int VideoId { get; set; }

        public Video Video { get; set; }

        [JsonConverter(typeof(StringEnumConverter))]
        public Priority Priority { get; set; }
    }
}
