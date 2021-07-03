using IVS.Data.DataContext;
using IVS.Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IVS.Domain.SerchService
{
    public class SerachService : ISerachService
    {
        private readonly ServiceDbContext serviceDbContext;

        public SerachService(ServiceDbContext serviceDbContext)
            => this.serviceDbContext = serviceDbContext;

        public async Task<IEnumerable<VideoViewModel>> GetUserVideos(int userId)
        {
            var userVideos = await serviceDbContext.Users.SelectMany(u => u.UsersToVideos.Where(u => u.UserId == userId).Select(s => new { s.VideoId, s.Priority })).ToListAsync();
            var userFlowVideos = await serviceDbContext.Users.SelectMany(u => u.UsersToFlows.Where(u => u.UserId == userId).Select(s => new { s.FlowId, s.Priority })).ToListAsync();
            var videosFlow = serviceDbContext.Flows.SelectMany(u => u.FlowsToVideos.Where(f => userFlowVideos.Select(s => s.FlowId).Contains(f.FlowId)).Select(s => new { s.FlowId, s.VideoId } ));

            var resultUserFlow = from userFlow in userFlowVideos
                                 join videoFlow in videosFlow on userFlow.FlowId equals videoFlow.FlowId
                                 select new { videoFlow.VideoId, userFlow.Priority };

            var userGroupsVideos = await serviceDbContext.Users.SelectMany(u => u.UsersToGroups.Where(u => u.UserId == userId).Select(s => s.GroupId)).ToListAsync();
            var groupVideos = await serviceDbContext.Groups.SelectMany(g => g.GroupsToVideos.Where(g => userGroupsVideos.Contains(g.GroupId)).Select(s => new { s.VideoId, s.Priority })).ToListAsync();

            userVideos.AddRange(resultUserFlow);
            userVideos.AddRange(groupVideos);
            var ordered = userVideos.GroupBy(s => s.VideoId).Select(s => new VideoViewModel { Priority = s.Max(s => s.Priority), VideoId = s.Key });

            return ordered;
            //return userVideos.Select(s => new VideoViewModel { Priority = s.Priority, VideoId = s.VideoId});
        }
    }
}
