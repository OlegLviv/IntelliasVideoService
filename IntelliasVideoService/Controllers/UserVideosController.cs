using IVS.Domain.SerchService;
using IVS.Domain.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelliasVideoService.Controllers
{
    [ApiController]
    [Route("users")]
    public class UserVideosController : ControllerBase
    {
        private readonly ISerachService serachService;

        public UserVideosController(ISerachService serachService)
        {
            this.serachService = serachService;
        }

        [HttpGet("{userId}/videos")]
        public async Task<IEnumerable<VideoViewModel>> GetUserVideosAsync([FromQuery] string priority, int userId)
        {
            var res = await serachService.GetUserVideosAsync(priority, userId);
            return res;
        }

        [HttpGet("{userId}/videos/{videoId}")]
        public async Task<IEnumerable<PathViewModel>> GetUserVideosPathAsync([FromQuery] string priority, int userId, int videoId)
        {
            var res = await serachService.GetUserVideosWithPathAsync(priority, userId, videoId);
            return res;
        }
    }
}
