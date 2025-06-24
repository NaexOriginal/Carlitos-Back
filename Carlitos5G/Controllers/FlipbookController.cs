using Carlitos5G.Dtos;
using Carlitos5G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlipbookController : ControllerBase
    {
        private readonly IFlipbookService _flipbookService;

        public FlipbookController(IFlipbookService flipbookService)
        {
            _flipbookService = flipbookService;
        }

        // GET: api/flipbook
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _flipbookService.GetAllFlipbooksAsync();
            return Ok(result);
        }

        // GET: api/flipbook/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var result = await _flipbookService.GetFlipbookByIdAsync(id);
            if (result.Data == null)
                return NotFound(result);

            return Ok(result);
        }

        // POST: api/flipbook
        [HttpPost]
        public async Task<IActionResult> Create([FromForm] FlipbookDto flipbookDto)
        {
            try
            {
                var result = await _flipbookService.CreateFlipbookAsync(flipbookDto);
                return CreatedAtAction(nameof(GetById), new { id = flipbookDto.Id }, flipbookDto);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    Inner = ex.InnerException?.Message
                });
            }
        }

        // PUT: api/flipbook/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromForm] FlipbookDto flipbookDto)
        {
            try
            {
                var result = await _flipbookService.UpdateFlipbookAsync(id, flipbookDto);
                if (!result.Data)
                    return NotFound(result);

                var updated = await _flipbookService.GetFlipbookByIdAsync(id);
                return Ok(updated.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    Message = ex.Message,
                    Inner = ex.InnerException?.Message
                });
            }
        }

        // DELETE: api/flipbook/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _flipbookService.DeleteFlipbookAsync(id);
            if (!result.Data)
                return NotFound(result);

            return NoContent();
        }

        // GET: api/flipbook/playlist/{playlistId}
        [HttpGet("playlist/{playlistId}")]
        public async Task<IActionResult> GetByPlaylistId(string playlistId)
        {
            var result = await _flipbookService.GetFlipbooksByPlaylistIdAsync(playlistId);
            if (result.Data == null || !result.Data.Any())
                return NotFound(new { Message = "No se encontraron flipbooks para la playlist indicada." });

            return Ok(result);
        }

        // GET: api/flipbook/{id}/pages
        [HttpGet("{id}/pages")]
        public async Task<IActionResult> GetFlipbookPages(string id)
        {
            var result = await _flipbookService.GetFlipbookPagesAsync(id);
            if (!result.Success || result.Data == null || !result.Data.Any())
                return NotFound(result);

            return Ok(result);
        }

    }
}
