using IVS.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IVS.Domain.SerchService
{
    public interface ISerachService
    {
        Task<IEnumerable<VideoViewModel>> GetUserVideosAsync(string priority, int userId);

        Task<IEnumerable<PathViewModel>> GetUserVideosWithPathAsync(string priority, int userId, int videoId);
    }
}