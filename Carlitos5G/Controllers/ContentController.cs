 using Carlitos5G.Dtos;
using Carlitos5G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentService _contentService;

        public ContentController(IContentService contentService)
        {
            _contentService = contentService;
        }

        // Obtener todos los contenidos
        [HttpGet]
        public async Task<IActionResult> GetContents()
        {
            var contents = await _contentService.GetAllContentsAsync();
            return Ok(contents);
        }

        // Obtener un contenido específico por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetContentById(string id)
        {
            var content = await _contentService.GetContentByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }
            return Ok(content);
        }

        // Crear un nuevo contenido
        [HttpPost]
        public async Task<IActionResult> CreateContent([FromForm] ContentDto contentDto)
        {
            try
            {
                var createdPlaylist = await _contentService.CreateContentAsync(contentDto);
                return CreatedAtAction(nameof(GetContentById), new { id = contentDto.Id }, contentDto);

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

        // Actualizar un contenido existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContent(string id, [FromForm] ContentDto contentDto)
        {

            contentDto.IsEdit = true;

            try
            {
                var updateResult = await _contentService.UpdateContentAsync(id, contentDto);
                if (!updateResult.Data)
                {
                    return NotFound();
                }

                var updatedPlaylist = await _contentService.GetContentByIdAsync(id);

                // Devuelve directamente el DTO sin envolver en otro objeto
                return Ok(updatedPlaylist.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Eliminar un contenido
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContent(string id)
        {
            var content = await _contentService.GetContentByIdAsync(id);
            if (content == null)
            {
                return NotFound();
            }

            await _contentService.DeleteContentAsync(id);
            return NoContent();
        }


        // Obtener todos los contenidos de una playlist específica
        [HttpGet("playlist/{playlistId}")]
        public async Task<IActionResult> GetContentsByPlaylistId(string playlistId)
        {
            var response = await _contentService.GetContentsByPlaylistIdAsync(playlistId);
            if (!response.Success)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }


        // Obtener estadísticas (likes y comentarios) de un contenido
        [HttpGet("{id}/stats")]
        public async Task<IActionResult> GetContentStats(string id)
        {
            var stats = await _contentService.GetContentStatsAsync(id);
            if (!stats.Success)
                return BadRequest(stats);

            return Ok(stats);
        }

        // Obtener todos los comentarios de un contenido
        [HttpGet("{id}/comments")]
        public async Task<IActionResult> GetCommentsByContentId(string id)
        {
            var comments = await _contentService.GetCommentsByContentIdAsync(id);
            if (!comments.Success)
                return BadRequest(comments);

            return Ok(comments);
        }



    }
}
