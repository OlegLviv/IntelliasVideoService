using System.Collections.Generic;

namespace IVS.Data.Entities
{
    public class Flow : Entity 
    {
        public List<UsersToFlows> UsersToFlows { get; set; }

        public List<GroupsToFlows> GroupsToFlows { get; set; }

        public List<FlowsToVideos> FlowsToVideos { get; set; }
    }
}
