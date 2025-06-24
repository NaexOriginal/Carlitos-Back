using Carlitos5G.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly ImageUploadService _imageUploadService;

        public ImageController(ImageUploadService imageUploadService)
        {
            _imageUploadService = imageUploadService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadImage(IFormFile imageFile)
        {
            try
            {
                // Llamar al servicio para subir la imagen
                var imageUrl = await _imageUploadService.UploadImageAsync(imageFile);
                return Ok(new { ImageUrl = imageUrl });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
    }
}
