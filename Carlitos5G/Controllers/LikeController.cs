using Carlitos5G.Dtos;
using Carlitos5G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }

        // Obtener todos los likes
        [HttpGet]
        public async Task<IActionResult> GetLikes()
        {
            var likes = await _likeService.GetAllLikesAsync();
            return Ok(likes);
        }

        // Obtener un like específico
        [HttpGet("{userId}/{tutorId}")]
        public async Task<IActionResult> GetLike(string userId, string tutorId, [FromQuery] string? contentId)
        {
            var like = await _likeService.GetLikeByIdAsync(userId, tutorId, contentId);
            if (like == null)
            {
                return NotFound();
            }
            return Ok(like);
        }

        // Crear un like
        [HttpPost]
        public async Task<IActionResult> CreateLike([FromBody] LikeDto likeDto)
        {
            if (likeDto == null)
            {
                return BadRequest("Like no válido.");
            }
            await _likeService.CreateLikeAsync(likeDto);
            return CreatedAtAction(nameof(GetLike), new { userId = likeDto.UserId, tutorId = likeDto.TutorId, contentId = likeDto.ContentId }, likeDto);
        }

        // Eliminar un like
        [HttpDelete("{userId}/{tutorId}")]
        public async Task<IActionResult> DeleteLike(string userId, string tutorId, [FromQuery] string? contentId)
        {
            var like = await _likeService.GetLikeByIdAsync(userId, tutorId, contentId);
            if (like == null)
            {
                return NotFound();
            }

            await _likeService.DeleteLikeAsync(userId, tutorId, contentId);
            return NoContent();
        }
    }
}
