using System.Collections.Generic;

namespace IVS.Data.Entities
{
    public class Group : Entity 
    {
        public List<UsersToGroups> UsersToGroups { get; set; }

        public List<GroupsToVideos> GroupsToVideos { get; set; }

        public List<GroupsToFlows> GroupsToFlows { get; set; }
    }
}
