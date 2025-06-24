using Carlitos5G.Dtos;
using Carlitos5G.Services;
using Microsoft.AspNetCore.Mvc;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvanceController : ControllerBase
    {
        private readonly IAvanceService _avanceService;

        public AvanceController(IAvanceService avanceService)
        {
            _avanceService = avanceService;
        }

        // Método para buscar los avances con filtros (userId, playlistId, contentId)
        [HttpGet("buscar")]
        public async Task<IActionResult> BuscarPorFiltro([FromQuery] string userId, [FromQuery] string? playlistId = null, [FromQuery] string? contentId = null)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("El parámetro 'userId' es obligatorio.");
            }

            var avances = await _avanceService.BuscarPorFiltroAsync(userId, playlistId, contentId);

            if (avances == null || avances.Data == null || !avances.Data.Any())

            {
                return NotFound("No se encontraron avances con los filtros proporcionados.");
            }

            return Ok(avances);
        }

        // Método para crear un nuevo avance
        [HttpPost("crear")]
        public async Task<IActionResult> CrearAvance([FromBody] AvanceDto avanceDto)
        {
            if (avanceDto == null)
            {
                return BadRequest("El objeto AvanceDto es necesario.");
            }

            // Llamamos al servicio para crear el avance
            var response = await _avanceService.CrearAvanceAsync(avanceDto);

            if (!response.Success)
            {
                return BadRequest(response.Message); // Maneja el error si la respuesta no es exitosa
            }

            return CreatedAtAction(nameof(CrearAvance), new { userId = response.Data.UserId, playlistId = response.Data.PlaylistId }, response.Data);
        }

    }
}
