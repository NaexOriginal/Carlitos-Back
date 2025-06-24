using Carlitos5G.Dtos;
using Carlitos5G.Commons;

namespace Carlitos5G.Services
{
    public interface IFlipbookService
    {
        Task<ServiceResponse<IEnumerable<FlipbookDto>>> GetAllFlipbooksAsync();
        Task<ServiceResponse<FlipbookDto>> GetFlipbookByIdAsync(string id);
        Task<ServiceResponse<FlipbookDto>> CreateFlipbookAsync(FlipbookDto dto);
        Task<ServiceResponse<bool>> UpdateFlipbookAsync(string id, FlipbookDto dto);
        Task<ServiceResponse<bool>> DeleteFlipbookAsync(string id);
        Task<ServiceResponse<IEnumerable<FlipbookDto>>> GetFlipbooksByPlaylistIdAsync(string playlistId);
        Task<ServiceResponse<IEnumerable<string>>> GetFlipbookPagesAsync(string id);

    }
}
