using Carlitos5G.Dtos;
using Carlitos5G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentService.GetAllCommentsAsync();
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(string id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPost]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto commentDto)
        {
            if (commentDto == null)
            {
                return BadRequest("Comentario no válido.");
            }
            await _commentService.CreateCommentAsync(commentDto);
            return CreatedAtAction(nameof(GetCommentById), new { id = commentDto.Id }, commentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(string id, [FromBody] CommentDto commentDto)
        {
            if (!Guid.TryParse(id, out Guid commentGuid))
            {
                return BadRequest("El ID proporcionado en la URL no tiene un formato GUID válido.");
            }

            commentDto.Id = commentGuid;

            var serviceResponse = await _commentService.UpdateCommentAsync(commentGuid, commentDto);

            if (!serviceResponse.Success)
            {
                if (serviceResponse.Message == "Comentario no encontrado.")
                {
                    return NotFound(serviceResponse.Message);
                }
                // Para cualquier otro error (ej. DbUpdateException capturada en el servicio)
                return BadRequest(serviceResponse.Message ?? "Error desconocido al actualizar el comentario.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(string id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }

            await _commentService.DeleteCommentAsync(id);
            return NoContent();
        }


    }
}
