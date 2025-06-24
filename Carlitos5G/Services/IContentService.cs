using Carlitos5G.Commons;
using Carlitos5G.Dtos;

namespace Carlitos5G.Services
{
    public interface IContentService
    {
        Task<ServiceResponse<IEnumerable<ContentDto>>> GetAllContentsAsync();
        Task<ServiceResponse<ContentDto>> GetContentByIdAsync(string id);
        Task<ServiceResponse<ContentDto>> CreateContentAsync(ContentDto contentDto);
        Task<ServiceResponse<bool>> UpdateContentAsync(string id, ContentDto contentDto);
        Task<ServiceResponse<bool>> DeleteContentAsync(string id);
        Task<ServiceResponse<IEnumerable<ContentDto>>> GetContentsByPlaylistIdAsync(string playlistId);

        Task<ServiceResponse<ContentStatsDto>> GetContentStatsAsync(string contentId);
        Task<ServiceResponse<IEnumerable<CommentDto>>> GetCommentsByContentIdAsync(string contentId);


    }
}
