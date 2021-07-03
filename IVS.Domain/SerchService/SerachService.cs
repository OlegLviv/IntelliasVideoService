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

        public async Task<IEnumerable<VideoViewModel>> GetUserVideosAsync(string priority, int userId)
        {
            var userVideos = await serviceDbContext.Users.SelectMany(u => u.UsersToVideos.Where(u => u.UserId == userId).Select(s => new { s.VideoId, s.Priority }))
                .ToListAsync();

            var userFlowsWithPriority = await serviceDbContext.Users.SelectMany(u => u.UsersToFlows.Where(u => u.UserId == userId).Select(s => new { s.FlowId, s.Priority }))
                .ToListAsync();

            var videoFlows = await serviceDbContext.Flows.SelectMany(u => u.FlowsToVideos.Where(f => userFlowsWithPriority.Select(s => s.FlowId).Contains(f.FlowId))
                .Select(s => new { s.FlowId, s.VideoId } ))
                .ToListAsync();

            var resultUserFlow = from userFlow in userFlowsWithPriority
                                 join videoFlow in videoFlows on userFlow.FlowId equals videoFlow.FlowId
                                 select new { videoFlow.VideoId, userFlow.Priority };

            var userGroups = await serviceDbContext.Users.SelectMany(u => u.UsersToGroups.Where(u => u.UserId == userId).Select(s => s.GroupId))
                .ToListAsync();

            var groupVideos = await serviceDbContext.Groups.SelectMany(g => g.GroupsToVideos.Where(g => userGroups.Contains(g.GroupId))
                .Select(s => new { s.VideoId, s.Priority }))
                .ToListAsync();

            var groupFlows = await serviceDbContext.Groups.SelectMany(g => g.GroupsToFlows.Where(g => userGroups.Contains(g.GroupId))
                .Select(s => new { s.FlowId, s.Priority }))
                .ToListAsync();

            var flowVideos = await serviceDbContext.Flows.SelectMany(u => u.FlowsToVideos.Where(f => groupFlows.Select(s => s.FlowId).Contains(f.FlowId))
                .Select(s => new { s.FlowId, s.VideoId }))
                .ToListAsync();

            var resultFlowVideos = from userFlow in groupFlows
                                 join videoFlow in flowVideos on userFlow.FlowId equals videoFlow.FlowId
                                 select new { videoFlow.VideoId, userFlow.Priority };

            userVideos.AddRange(resultUserFlow);
            userVideos.AddRange(groupVideos);
            userVideos.AddRange(resultFlowVideos);

            var ordered = userVideos.GroupBy(s => s.VideoId).Select(s => new VideoViewModel { Priority = s.Max(s => s.Priority), VideoId = s.Key });

            return priority == "asc" ? ordered.OrderBy(s => s.Priority) : ordered.OrderByDescending(s => s.Priority);
        }

        public async Task<IEnumerable<PathViewModel>> GetUserVideosWithPathAsync(string priority, int userId, int videoId)
        {
            var userVideosPath = await serviceDbContext.Users.SelectMany(u => 
                u.UsersToVideos.Where(u => u.UserId == userId && u.VideoId == videoId)
                    .Select(s => new { s.Priority, Path = $"users/{s.UserId}/videos/{s.VideoId}" }))
                .ToListAsync();

            var userFlows = await serviceDbContext.Users.SelectMany(u => 
                u.UsersToFlows.Where(u => u.UserId == userId)
                    .Select(s => new { s.FlowId, s.Priority }))
                .ToListAsync();

            var videosFlow = await serviceDbContext.Flows.SelectMany(u => u.FlowsToVideos.Where(s => s.VideoId == videoId).Select(s => new { s.FlowId, s.VideoId })).ToListAsync();
            
            var joinedVideosFlow = videosFlow.Join(userFlows, s => s.FlowId, s => s.FlowId, (s1, s2) => new { s2.Priority, userId, s1.FlowId, s1.VideoId });

            var userGroupsVideos = await serviceDbContext.Users.SelectMany(u => u.UsersToGroups.Where(u => u.UserId == userId).Select(s => s.GroupId)).ToListAsync();

            var groupFlows = await serviceDbContext.Flows.SelectMany(s => s.GroupsToFlows.Where(f => userGroupsVideos.Contains(f.GroupId)).Select(f => new { f.Priority, f.GroupId, f.FlowId }))
                .ToListAsync();

            var groupFlowVideos = await serviceDbContext.Flows.SelectMany(f => 
                f.FlowsToVideos.Where(f => f.VideoId == videoId && groupFlows.Select(s => s.FlowId).Contains(f.FlowId))
                    .Select(s => new { s.FlowId, s.VideoId }))
                .ToListAsync();

            var joinedFlowGroupVideos = groupFlows.Join(groupFlowVideos, s => s.FlowId, s => s.FlowId, (s1, s2) => 
                new { s1.Priority, Path = $"users/{userId}/groups/{s1.GroupId}/flows/{s1.FlowId}/videos/{s2.VideoId}" });

            var groupVideosPath = await serviceDbContext.Groups.SelectMany(g => g.GroupsToVideos.Where(g => g.VideoId == videoId && userGroupsVideos.Contains(g.GroupId))
                .Select(s => new { s.Priority, Path = $"users/{userId}/groups/{s.GroupId}/videos/{s.VideoId}" }))
                .ToListAsync();

            var flowsWithPath = joinedVideosFlow.Select(s => new { s.Priority, Path = $"users/{userId}/flows/{s.FlowId}/videos/{s.VideoId}" }).ToList();

            userVideosPath.AddRange(joinedFlowGroupVideos);
            userVideosPath.AddRange(flowsWithPath);
            userVideosPath.AddRange(groupVideosPath);

            var viewModel = userVideosPath.Select(s => new PathViewModel { Path = s.Path, Priority = s.Priority });

            return priority == "asc" ? viewModel.OrderBy(s => s.Priority) : viewModel.OrderByDescending(s => s.Priority);
        }
    }
}
