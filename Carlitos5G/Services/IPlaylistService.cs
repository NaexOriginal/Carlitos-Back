using Carlitos5G.Commons;
using Carlitos5G.Dtos;

namespace Carlitos5G.Services
{
    public interface IPlaylistService
    {
        Task<ServiceResponse<IEnumerable<PlaylistDto>>> GetAllPlaylistsAsync();
        Task<ServiceResponse<PlaylistDto>> GetPlaylistByIdAsync(string id);
        Task<ServiceResponse<PlaylistDto>> CreatePlaylistAsync(PlaylistDto playlistDto);
        Task<ServiceResponse<bool>> UpdatePlaylistAsync(string id, PlaylistDto playlistDto);
        Task<ServiceResponse<bool>> DeletePlaylistAsync(string id);
    }
}
