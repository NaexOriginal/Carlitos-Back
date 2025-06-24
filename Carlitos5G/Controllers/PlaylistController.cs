using Carlitos5G.Commons;
using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Services;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Carlitos5G.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistController : ControllerBase
    {
        private readonly IPlaylistService _playlistService;
        private readonly ApplicationDbContext _dbContext;


        public PlaylistController(IPlaylistService playlistService, ApplicationDbContext dbContext)
        {
            _playlistService = playlistService;
            _dbContext = dbContext;
        }

        // Obtener todas las playlists
        [HttpGet]
        public async Task<IActionResult> GetPlaylists()
        {
            var playlists = await _playlistService.GetAllPlaylistsAsync();
            return Ok(playlists);
        }

        // Obtener una playlist específica por su ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaylistById(string id)
        {
            var playlist = await _playlistService.GetPlaylistByIdAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }
            return Ok(playlist);
        }

        // Crear una nueva playlist
        [HttpPost]
        public async Task<IActionResult> CreatePlaylist([FromForm] PlaylistDto playlistDto)
        {
            try
            {
                var createdPlaylist = await _playlistService.CreatePlaylistAsync(playlistDto);
                return CreatedAtAction(nameof(GetPlaylistById), new { id = createdPlaylist.Data.Id }, createdPlaylist.Data);
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

        // Actualizar una playlist existente
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlaylist(string id, [FromForm] PlaylistDto playlistDto)
        {
            playlistDto.IsEdit = true;

            try
            {
                var updateResult = await _playlistService.UpdatePlaylistAsync(id, playlistDto);
                if (!updateResult.Data)
                {
                    return NotFound();
                }

                var updatedPlaylist = await _playlistService.GetPlaylistByIdAsync(id);

                // Devuelve directamente el DTO sin envolver en otro objeto
                return Ok(updatedPlaylist.Data);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        // Eliminar una playlist
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlaylist(string id)
        {
            var playlist = await _playlistService.GetPlaylistByIdAsync(id);
            if (playlist == null)
            {
                return NotFound();
            }

            await _playlistService.DeletePlaylistAsync(id);
            return NoContent();
        }

        // En PlaylistController.cs
        [HttpPut("{playlistId}/reassign")]
        public async Task<IActionResult> ReassignPlaylist(Guid playlistId, [FromBody] ReassignPlaylistRequest request)
        {
            var response = new ServiceResponse<bool>();

            try
            {
                var playlist = await _dbContext.Playlists.FindAsync(playlistId);
                if (playlist == null)
                {
                    response.Success = false;
                    response.Message = "Playlist no encontrada";
                    return NotFound(response);
                }

                var newTutor = await _dbContext.Tutors.FindAsync(request.NewTutorId);
                if (newTutor == null)
                {
                    response.Success = false;
                    response.Message = "Tutor destino no encontrado";
                    return NotFound(response);
                }

                // Guardar el tutor anterior si necesitas historial
                var oldTutorId = playlist.TutorId;

                // Reasignar
                playlist.TutorId = request.NewTutorId;
                playlist.UpdationDate = DateTime.UtcNow;

                await _dbContext.SaveChangesAsync();

                // Opcional: Aquí podrías registrar el cambio en una tabla de historial
                // await _context.PlaylistHistory.AddAsync(new PlaylistHistory {
                //     PlaylistId = playlistId,
                //     OldTutorId = oldTutorId,
                //     NewTutorId = request.NewTutorId,
                //     ChangeDate = DateTime.UtcNow
                // });

                response.Data = true;
                response.Message = "Playlist reasignada correctamente";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al reasignar playlist";
                response.ErrorDetails = ex.Message;
                return StatusCode(500, response);
            }
        }

        [HttpGet("with-tutors")]
        public async Task<IActionResult> GetPlaylistsWithTutors()
        {
            var response = new ServiceResponse<IEnumerable<PlaylistWithTutorDto>>();

            try
            {
                var playlists = await _dbContext.Playlists
                    .Include(p => p.Tutor) // Asegúrate de tener esta relación en tu modelo
                    .Select(p => new PlaylistWithTutorDto
                    {
                        PlaylistId = p.Id,
                        Title = p.Title,
                        CurrentTutorId = p.TutorId,
                        CurrentTutorName = p.Tutor.Name,
                        Status = p.Status,
                        ThumbnailUrl = p.Thumb,
                        LastUpdated = p.UpdationDate
                    })
                    .ToListAsync();

                response.Data = playlists;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener playlists";
                response.ErrorDetails = ex.Message;
                return StatusCode(500, response);
            }
        }


    }
}
