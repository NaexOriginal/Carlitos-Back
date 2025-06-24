using Carlitos5G.Dtos;
using Carlitos5G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookmarkController : ControllerBase
    {
        private readonly IBookmarkService _bookmarkService;

        public BookmarkController(IBookmarkService bookmarkService)
        {
            _bookmarkService = bookmarkService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookmarks()
        {
            var result = await _bookmarkService.GetAllBookmarksAsync();
            return Ok(result);
        }

        [HttpGet("{userId}/{playlistId}")]
        public async Task<IActionResult> GetBookmark(string userId, string playlistId)
        {
            var result = await _bookmarkService.GetBookmarkAsync(userId, playlistId);
            if (!result.Success)
                return NotFound(result);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBookmark([FromBody] BookmarkDto bookmarkDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _bookmarkService.CreateBookmarkAsync(bookmarkDto);
            return CreatedAtAction(nameof(GetBookmark), new { userId = bookmarkDto.UserId, playlistId = bookmarkDto.PlaylistId }, result);
        }

        [HttpDelete("{userId}/{playlistId}")]
        public async Task<IActionResult> DeleteBookmark(string userId, string playlistId)
        {
            var result = await _bookmarkService.DeleteBookmarkAsync(userId, playlistId);
            if (!result.Success)
                return NotFound(result);

            return NoContent();
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBookmarksByUserId(string userId)
        {
            var result = await _bookmarkService.GetBookmarksByUserIdAsync(userId);
            return Ok(result);
        }
    }
}
