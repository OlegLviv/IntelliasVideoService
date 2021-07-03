using IVS.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IVS.Domain.SerchService
{
    public interface ISerachService
    {
        Task<IEnumerable<VideoViewModel>> GetUserVideosAsync(int userId);

        Task<IEnumerable<PathViewModel>> GetUserVideosWithPathAsync(int userId, int videoId);
    }
}