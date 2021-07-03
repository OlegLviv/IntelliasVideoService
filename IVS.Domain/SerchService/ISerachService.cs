using IVS.Domain.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IVS.Domain.SerchService
{
    public interface ISerachService
    {
        Task<IEnumerable<VideoViewModel>> GetUserVideos(int userId);
    }
}