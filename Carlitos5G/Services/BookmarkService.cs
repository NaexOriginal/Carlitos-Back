using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Carlitos5G.Commons;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Services
{
    public class BookmarkService : IBookmarkService
    {
        private readonly ApplicationDbContext _context;

        public BookmarkService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<BookmarkDto>>> GetAllBookmarksAsync()
        {
            var response = new ServiceResponse<IEnumerable<BookmarkDto>>();
            try
            {
                var bookmarks = await _context.Bookmarks
                    .Select(b => new BookmarkDto
                    {
                        UserId = b.UserId,
                        PlaylistId = b.PlaylistId
                    })
                    .ToListAsync();

                response.Data = bookmarks;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los bookmarks.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<BookmarkDto>> GetBookmarkAsync(string userId, string playlistId)
        {
            var response = new ServiceResponse<BookmarkDto>();
            try
            {
                var bookmark = await _context.Bookmarks
                    .Where(b => b.UserId.ToString() == userId && b.PlaylistId.ToString() == playlistId)
                    .Select(b => new BookmarkDto
                    {
                        UserId = b.UserId,
                        PlaylistId = b.PlaylistId
                    })
                    .FirstOrDefaultAsync();

                if (bookmark == null)
                {
                    response.Success = false;
                    response.Message = "Bookmark no encontrado.";
                }
                else
                {
                    response.Data = bookmark;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el bookmark.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<BookmarkDto>> CreateBookmarkAsync(BookmarkDto bookmarkDto)
        {
            var response = new ServiceResponse<BookmarkDto>();
            try
            {
                var bookmark = new Bookmark
                {
                    UserId = bookmarkDto.UserId,
                    PlaylistId = bookmarkDto.PlaylistId
                };

                _context.Bookmarks.Add(bookmark);
                await _context.SaveChangesAsync();

                response.Data = bookmarkDto;
                response.Message = "Bookmark creado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el bookmark.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteBookmarkAsync(string userId, string playlistId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var bookmark = await _context.Bookmarks
                    .FirstOrDefaultAsync(b => b.UserId.ToString() == userId && b.PlaylistId.ToString() == playlistId);

                if (bookmark == null)
                {
                    response.Success = false;
                    response.Message = "Bookmark no encontrado.";
                    response.Data = false;
                    return response;
                }

                _context.Bookmarks.Remove(bookmark);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Bookmark eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el bookmark.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<BookmarkDto>>> GetBookmarksByUserIdAsync(string userId)
        {
            var response = new ServiceResponse<IEnumerable<BookmarkDto>>();
            try
            {
                if (!Guid.TryParse(userId, out Guid userGuid))
                {
                    response.Success = false;
                    response.Message = "ID de usuario inválido.";
                    return response;
                }

                var bookmarks = await _context.Bookmarks
                    .Where(b => b.UserId == userGuid)
                    .Select(b => new BookmarkDto
                    {
                        UserId = b.UserId,
                        PlaylistId = b.PlaylistId
                    })
                    .ToListAsync();

                response.Data = bookmarks;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los bookmarks por usuario.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }
    }
}
