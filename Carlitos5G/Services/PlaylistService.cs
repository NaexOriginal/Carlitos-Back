using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Carlitos5G.Commons;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Carlitos5G.Services
{
    public class PlaylistService : IPlaylistService
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageUploadService _imageUploadService;


        public PlaylistService(ApplicationDbContext context, ImageUploadService imageUploadService)
        {
            _context = context;
            _imageUploadService = imageUploadService;
        }

        public async Task<ServiceResponse<IEnumerable<PlaylistDto>>> GetAllPlaylistsAsync()
        {
            var response = new ServiceResponse<IEnumerable<PlaylistDto>>();
            try
            {
                var playlists = await _context.Playlists
                    .Select(p => new PlaylistDto
                    {
                        Id = p.Id,
                        TutorId = p.TutorId,
                        Title = p.Title,
                        Description = p.Description,
                        Thumb = p.Thumb,
                        Date = p.Date,
                        Status = p.Status,
                        Categoria = p.Categoria,
                        Iframe = p.Iframe,
                        UpdationDate = p.UpdationDate,
                        IsDiplomado = p.IsDiplomado
                    })
                    .ToListAsync();

                response.Data = playlists;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener las playlists.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<PlaylistDto>> GetPlaylistByIdAsync(string id)
        {
            var response = new ServiceResponse<PlaylistDto>();
            try
            {
                var playlist = await _context.Playlists
                    .Where(p => p.Id.ToString() == id)
                    .Select(p => new PlaylistDto
                    {
                        Id = p.Id,
                        TutorId = p.TutorId,
                        Title = p.Title,
                        Description = p.Description,
                        Thumb = p.Thumb,
                        Date = p.Date,
                        Status = p.Status,
                        Categoria = p.Categoria,
                        Iframe = p.Iframe,
                        UpdationDate = p.UpdationDate,
                        IsDiplomado = p.IsDiplomado
                    })
                    .FirstOrDefaultAsync();

                if (playlist == null)
                {
                    response.Success = false;
                    response.Message = "Playlist no encontrada.";
                }
                else
                {
                    response.Data = playlist;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener la playlist.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<PlaylistDto>> CreatePlaylistAsync(PlaylistDto playlistDto)
        {
            var response = new ServiceResponse<PlaylistDto>();
            try
            {

                string? imageUrl = null;
                if (playlistDto.ImageFile != null)
                {
                    imageUrl = await _imageUploadService.UploadImageAsync(playlistDto.ImageFile);
                }

                var playlist = new Playlist
                {
                    Id = playlistDto.Id,
                    TutorId = playlistDto.TutorId,
                    Title = playlistDto.Title,
                    Description = playlistDto.Description,
                    Thumb = imageUrl,
                    Date = playlistDto.Date,
                    Status = playlistDto.Status,
                    Categoria = playlistDto.Categoria,
                    Iframe = playlistDto.Iframe,
                    UpdationDate = playlistDto.UpdationDate,
                    IsDiplomado = playlistDto.IsDiplomado
                };

                _context.Playlists.Add(playlist);
                await _context.SaveChangesAsync();

                response.Data = playlistDto;
                response.Message = "Playlist creada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear la playlist.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdatePlaylistAsync(string id, PlaylistDto playlistDto)
        {

            var response = new ServiceResponse<bool>();
            try
            {
                var playlist = await _context.Playlists.FindAsync(Guid.Parse(id));
                if (playlist == null)
                {
                    response.Success = false;
                    response.Message = "Playlist no encontrada.";
                    response.Data = false;
                    return response;
                }

                playlist.TutorId = playlistDto.TutorId;
                playlist.Title = playlistDto.Title;
                playlist.Description = playlistDto.Description;
                playlist.Date = playlistDto.Date;
                playlist.Status = playlistDto.Status;
                playlist.Categoria = playlistDto.Categoria;
                playlist.Iframe = playlistDto.Iframe;
                playlist.UpdationDate = playlistDto.UpdationDate;
                playlist.IsDiplomado = playlistDto.IsDiplomado;

                if (playlistDto.ImageFile != null)
                {
                    string imageUrl = await _imageUploadService.UploadImageAsync(playlistDto.ImageFile);
                    playlist.Thumb = imageUrl;
                }

                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Playlist actualizada exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar la playlist.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeletePlaylistAsync(string id)
        {
            var response = new ServiceResponse<bool>();

            // Use transaction to ensure all operations complete or none do
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Find the playlist
                var playlist = await _context.Playlists
                    .Include(p => p.Contents) // Make sure you have this navigation property
                    .FirstOrDefaultAsync(p => p.Id.ToString() == id);

                if (playlist == null)
                {
                    response.Success = false;
                    response.Message = "Playlist no encontrada.";
                    response.Data = false;
                    return response;
                }

                // Get all content IDs in the playlist for cascading deletions
                var contentIds = playlist.Contents.Select(c => c.Id).ToList();

                // 1. Delete likes associated with content
                var likes = await _context.Likes
                    .Where(l => contentIds.Contains((Guid)l.ContentId))
                    .ToListAsync();
                _context.Likes.RemoveRange(likes);

                // 2. Delete comments associated with content
                var comments = await _context.Comments
                    .Where(c => contentIds.Contains((Guid)c.ContentId))
                    .ToListAsync();
                _context.Comments.RemoveRange(comments);

                // 3. Delete file system resources
                foreach (var content in playlist.Contents)
                {
                    // Delete the actual video files
                    if (!string.IsNullOrEmpty(content.MediaPath))
                    {
                        string videoPath = Path.Combine("uploaded_files", content.MediaPath);
                        if (File.Exists(videoPath))
                        {
                            File.Delete(videoPath);
                        }
                    }

                    // Delete thumbnail files
                    if (!string.IsNullOrEmpty(content.ThumbnailPath))
                    {
                        string thumbPath = Path.Combine("uploaded_files", content.ThumbnailPath);
                        if (File.Exists(thumbPath))
                        {
                            File.Delete(thumbPath);
                        }
                    }
                }

                // 4. Delete playlist thumbnail
                if (!string.IsNullOrEmpty(playlist.Thumb))
                {
                    string playlistThumbPath = Path.Combine("uploaded_files", playlist.Thumb);
                    if (File.Exists(playlistThumbPath))
                    {
                        File.Delete(playlistThumbPath);
                    }
                }

                // 5. Delete bookmarks
                var bookmarks = await _context.Bookmarks
                    .Where(b => b.PlaylistId == playlist.Id)
                    .ToListAsync();
                _context.Bookmarks.RemoveRange(bookmarks);

                // 6. Delete content records
                var contents = await _context.Contents
                    .Where(c => c.PlaylistId == playlist.Id)
                    .ToListAsync();
                _context.Contents.RemoveRange(contents);

                // 7. Finally, delete the playlist itself
                _context.Playlists.Remove(playlist);

                // Save all changes
                await _context.SaveChangesAsync();

                // Commit transaction
                await transaction.CommitAsync();

                response.Data = true;
                response.Message = "Playlist y todos los recursos asociados eliminados exitosamente.";
            }
            catch (Exception ex)
            {
                // Roll back transaction in case of error
                await transaction.RollbackAsync();

                response.Success = false;
                response.Message = "Error al eliminar la playlist.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }

            return response;
        }
    
    }
}



