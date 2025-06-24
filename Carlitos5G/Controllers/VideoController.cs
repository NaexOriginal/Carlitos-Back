using Carlitos5G.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private readonly VideoUploadService _videoUploadService;

        public VideoController(VideoUploadService videoUploadService)
        {
            _videoUploadService = videoUploadService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadVideo(IFormFile videoFile)
        {
            try
            {
                // Llamar al servicio para subir el video
                var videoUrl = await _videoUploadService.UploadVideoAsync(videoFile);
                return Ok(new { VideoUrl = videoUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
