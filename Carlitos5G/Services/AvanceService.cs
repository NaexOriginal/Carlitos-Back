using Carlitos5G.Commons;
using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Services
{
    public class AvanceService : IAvanceService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IAuditService _auditService;


        public AvanceService(ApplicationDbContext dbContext, IAuditService auditService)
        {
            _dbContext = dbContext;
            _auditService = auditService;
        }

        public async Task<ServiceResponse<List<AvanceDto>>> BuscarPorFiltroAsync(string userId, string? playlistId = null, string? contentId = null)
        {
            var response = new ServiceResponse<List<AvanceDto>>();

            try
            {
                var query = _dbContext.Avances.AsQueryable();

                if (!Guid.TryParse(userId, out var parsedUserId))
                {
                    response.Success = false;
                    response.Message = "ID de usuario no válido.";
                    return response;
                }

                query = query.Where(a => a.UserId == parsedUserId);

                if (!string.IsNullOrEmpty(playlistId) && Guid.TryParse(playlistId, out var parsedPlaylistId))
                {
                    query = query.Where(a => a.PlaylistId == parsedPlaylistId);
                }

                if (!string.IsNullOrEmpty(contentId) && Guid.TryParse(contentId, out var parsedContentId))
                {
                    query = query.Where(a => a.ContentId == parsedContentId);
                }

                var avances = await query
                    .Select(a => new AvanceDto
                    {
                        PlaylistId = a.PlaylistId,
                        ContentId = a.ContentId,
                        UserId = a.UserId,
                        Type = a.Type
                    })
                    .ToListAsync();

                response.Data = avances; // Aquí asignas los avances encontrados a la propiedad Data
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al buscar avances.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }


        public async Task<ServiceResponse<AvanceDto>> CrearAvanceAsync(AvanceDto avanceDto)
        {
            var response = new ServiceResponse<AvanceDto>();

            try
            {
                var avance = new Avance
                {
                    PlaylistId = avanceDto.PlaylistId,
                    ContentId = avanceDto.ContentId,
                    UserId = avanceDto.UserId,
                    Type = avanceDto.Type
                };

                _dbContext.Avances.Add(avance);
                await _dbContext.SaveChangesAsync();

                var newData = System.Text.Json.JsonSerializer.Serialize(avance);
                await _auditService.LogAsync("Avance", "Create", avance.UserId.ToString(), newData, "system");

                response.Data = avanceDto;
                response.Message = "Avance creado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el avance.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }
    }
}
