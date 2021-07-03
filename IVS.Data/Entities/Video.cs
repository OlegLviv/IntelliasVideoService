using System.Collections.Generic;

namespace IVS.Data.Entities
{
    public class Video : Entity 
    {
        public string Name { get; set; }

        public List<UsersToVideos> UsersToVideos { get; set; }

        public List<GroupsToVideos> GroupsToVideos { get; set; }

        public List<FlowsToVideos> FlowsToVideos { get; set; }
    }
}
