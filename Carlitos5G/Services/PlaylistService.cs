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
                    Date = DateTime.UtcNow,
                    Status = playlistDto.Status,
                    Categoria = playlistDto.Categoria,
                    Iframe = playlistDto.Iframe,
                    UpdationDate = DateTime.UtcNow,
                    IsDiplomado = playlistDto.IsDiplomado
                };

                _context.Playlists.Add(playlist);
                await _context.SaveChangesAsync();

                response.Data = new PlaylistDto
                {
                    Id = playlist.Id,
                    TutorId = playlist.TutorId,
                    Title = playlist.Title,
                    Description = playlist.Description,
                    Thumb = playlist.Thumb,
                    Date = playlist.Date,
                    Status = playlist.Status,
                    Categoria = playlist.Categoria,
                    Iframe = playlist.Iframe,
                    UpdationDate = playlist.UpdationDate,
                    IsDiplomado = playlist.IsDiplomado
                };
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
                playlist.Status = playlistDto.Status;
                playlist.Categoria = playlistDto.Categoria;
                playlist.Iframe = playlistDto.Iframe;
                playlist.UpdationDate = DateTime.UtcNow;
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

        public async Task<ServiceResponse<bool>> DeletePlaylistAsync(string idString)
        {
            var response = new ServiceResponse<bool>();

            if (!Guid.TryParse(idString, out Guid idToDelete))
            {
                response.Success = false;
                response.Message = "Formato de ID de playlist invalido";
                response.Data = false;
                return response;
            }

            // Use transaction to ensure all operations complete or none do
            using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                // Find the playlist
                var playlist = await _context.Playlists
                    .Include(p => p.Contents) // Make sure you have this navigation property
                    .FirstOrDefaultAsync(p => p.Id == idToDelete);

                if (playlist == null)
                {
                    response.Success = false;
                    response.Message = "Playlist no encontrada.";
                    response.Data = false;
                    return response;
                }

                var uploadedFilesPath = Path.Combine(Directory.GetCurrentDirectory(), "uploaded_files");

                if (!string.IsNullOrEmpty(playlist.Thumb) && playlist.Thumb.StartsWith("/images/"))
                {
                    string fileName = playlist.Thumb.Replace("/images/", "");
                    string playlistThumbPath = Path.Combine(uploadedFilesPath, fileName);
                    if (File.Exists(playlistThumbPath))
                    {
                        File.Delete(playlistThumbPath);
                        Console.WriteLine($"Deleted playlist thumbnail: {playlistThumbPath}");
                    }
                    else
                    {
                        Console.WriteLine($"Playlist thumbnail not found: {playlistThumbPath}");
                    }
                }

                if (playlist.Contents != null && playlist.Contents.Any())
                {
                    var contentIds = playlist.Contents.Select(c => c.Id).ToList();

                    // Delete likes associated with content
                    var likes = await _context.Likes
                        .Where(l => l.ContentId.HasValue && contentIds.Contains(l.ContentId.Value))
                        .ToListAsync();
                    _context.Likes.RemoveRange(likes);
                    Console.WriteLine($"Deleted {likes.Count} likes.");

                    // Delete comments associated with content
                    var comments = await _context.Comments
                        .Where(c => c.ContentId.HasValue && contentIds.Contains(c.ContentId.Value))
                        .ToListAsync();
                    _context.Comments.RemoveRange(comments);
                    Console.WriteLine($"Deleted {comments.Count} comments.");

                    // Delete file system resources for each content
                    foreach (var content in playlist.Contents)
                    {
                        // Delete the actual video files
                        if (!string.IsNullOrEmpty(content.MediaPath) && content.MediaPath.StartsWith("/videos/")) // Asume "/videos/" para tus archivos de video
                        {
                            string videoFileName = content.MediaPath.Replace("/videos/", "");
                            string videoPath = Path.Combine(uploadedFilesPath, videoFileName);
                            if (File.Exists(videoPath))
                            {
                                File.Delete(videoPath);
                                Console.WriteLine($"Deleted content video: {videoPath}");
                            }
                            else
                            {
                                Console.WriteLine($"Content video not found: {videoPath}");
                            }
                        }

                        // Delete thumbnail files
                        if (!string.IsNullOrEmpty(content.ThumbnailPath) && content.ThumbnailPath.StartsWith("/content_thumbs/")) // Asume "/content_thumbs/" para tus miniaturas de contenido
                        {
                            string thumbFileName = content.ThumbnailPath.Replace("/content_thumbs/", "");
                            string contentThumbPath = Path.Combine(uploadedFilesPath, thumbFileName);
                            if (File.Exists(contentThumbPath))
                            {
                                File.Delete(contentThumbPath);
                                Console.WriteLine($"Deleted content thumbnail: {contentThumbPath}");
                            }
                            else
                            {
                                Console.WriteLine($"Content thumbnail not found: {contentThumbPath}");
                            }
                        }
                    }

                    _context.Contents.RemoveRange(playlist.Contents); // Ya los cargamos con .Include()
                    Console.WriteLine($"Deleted {playlist.Contents.Count} content records.");
                }
                else
                {
                    Console.WriteLine("No contents associated with this playlist to delete.");
                }

                var bookmarks = await _context.Bookmarks
                    .Where(b => b.PlaylistId == playlist.Id)
                    .ToListAsync();
                _context.Bookmarks.RemoveRange(bookmarks);
                Console.WriteLine($"Deleted {bookmarks.Count} bookmarks.");


                // Finally, delete the playlist itself
                _context.Playlists.Remove(playlist);
                Console.WriteLine($"Deleted playlist record: {playlist.Id}");

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();

                response.Data = true;
                response.Message = "Playlist y todos los recursos asociados eliminados exitosamente.";
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();

                response.Success = false;
                response.Message = "Error al eliminar la playlist.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
                // Agrega un log para ver la excepción real en la consola del servidor
                Console.WriteLine($"Error deleting playlist: {ex.Message}");
                Console.WriteLine($"Inner Exception: {ex.InnerException?.Message}");
            }

            return response;
        }

    }
}



