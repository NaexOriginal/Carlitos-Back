using Carlitos5G.Commons;
using Carlitos5G.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carlitos5G.Services
{
    public interface IBookmarkService
    {
        Task<ServiceResponse<IEnumerable<BookmarkDto>>> GetAllBookmarksAsync();
        Task<ServiceResponse<BookmarkDto>> GetBookmarkAsync(string userId, string playlistId);
        Task<ServiceResponse<BookmarkDto>> CreateBookmarkAsync(BookmarkDto bookmarkDto);
        Task<ServiceResponse<bool>> DeleteBookmarkAsync(string userId, string playlistId);
        Task<ServiceResponse<IEnumerable<BookmarkDto>>> GetBookmarksByUserIdAsync(string userId);
    }
}
