using Carlitos5G.Commons;
using Carlitos5G.Dtos;

namespace Carlitos5G.Services
{
    public interface IAvanceService
    {
        Task<ServiceResponse<List<AvanceDto>>> BuscarPorFiltroAsync(string userId, string? playlistId = null, string? contentId = null);
        Task<ServiceResponse<AvanceDto>> CrearAvanceAsync(AvanceDto avanceDto);
    }
}
