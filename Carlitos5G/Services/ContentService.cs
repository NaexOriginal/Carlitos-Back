using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Carlitos5G.Commons;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Services
{
    public class ContentService : IContentService
    {
        private readonly ApplicationDbContext _context;
        private readonly ImageUploadService _imageUploadService;
        private readonly VideoUploadService _videoUploadService;


        public ContentService(ApplicationDbContext context, ImageUploadService imageUploadService, VideoUploadService videoUploadService)
        {
            _context = context;
            _imageUploadService = imageUploadService;
            _videoUploadService = videoUploadService;
        }

        public async Task<ServiceResponse<IEnumerable<ContentDto>>> GetAllContentsAsync()
        {
            var response = new ServiceResponse<IEnumerable<ContentDto>>();
            try
            {
                var contents = await _context.Contents
                    .Select(c => new ContentDto
                    {
                        Id = c.Id,
                        TutorId = c.TutorId,
                        PlaylistId = c.PlaylistId,
                        Title = c.Title,
                        Description = c.Description,
                        MediaPath = c.MediaPath,
                        ThumbnailPath = c.ThumbnailPath,
                        Date = c.Date,
                        Status = c.Status,
                        Type = c.Type
                    }).ToListAsync();

                response.Data = contents;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los contenidos.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<ContentDto>> GetContentByIdAsync(string id)
        {
            var response = new ServiceResponse<ContentDto>();
            try
            {
                var content = await _context.Contents
                    .Where(c => c.Id.ToString() == id)
                    .Select(c => new ContentDto
                    {
                        Id = c.Id,
                        TutorId = c.TutorId,
                        PlaylistId = c.PlaylistId,
                        Title = c.Title,
                        Description = c.Description,
                        MediaPath = c.MediaPath,
                        ThumbnailPath = c.ThumbnailPath,
                        Date = c.Date,
                        Status = c.Status,
                        Type = c.Type
                    }).FirstOrDefaultAsync();

                if (content == null)
                {
                    response.Success = false;
                    response.Message = "Contenido no encontrado.";
                }
                else
                {
                    response.Data = content;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el contenido.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<ContentDto>> CreateContentAsync(ContentDto contentDto)
        {
            var response = new ServiceResponse<ContentDto>();
            try
            {

                string? videoUrl = null;
                if (contentDto.MediaFile != null)
                    videoUrl = await _videoUploadService.UploadVideoAsync(contentDto.MediaFile);

                string? imageUrl = null;
                if (contentDto.ThumbnailFile != null)
                    imageUrl = await _imageUploadService.UploadImageAsync(contentDto.ThumbnailFile);

                var content = new Content
                {
                    Id = contentDto.Id,
                    TutorId = contentDto.TutorId,
                    PlaylistId = contentDto.PlaylistId,
                    Title = contentDto.Title,
                    Description = contentDto.Description,
                    MediaPath = videoUrl ?? contentDto.MediaPath,
                    ThumbnailPath = imageUrl ?? contentDto.ThumbnailPath,
                    Date = contentDto.Date,
                    Status = contentDto.Status,
                    Type = contentDto.Type
                };

                _context.Contents.Add(content);
                await _context.SaveChangesAsync();

                response.Data = contentDto;
                response.Message = "Contenido creado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el contenido.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> UpdateContentAsync(string id, ContentDto contentDto)
        {
            var response = new ServiceResponse<bool>();
            try
            {

                if (!Guid.TryParse(id, out Guid contentGuid))
                {
                    response.Success = false;
                    response.Message = "El formato del ID proporcionado no es válido.";
                    response.Data = false;
                    return response;
                }

                var content = await _context.Contents.FindAsync(contentGuid);
                if (content == null)
                {
                    response.Success = false;
                    response.Message = "Contenido no encontrado.";
                    response.Data = false;
                    return response;
                }

                content.TutorId = contentDto.TutorId;
                content.PlaylistId = contentDto.PlaylistId;
                content.Title = contentDto.Title;
                content.Description = contentDto.Description;
                content.Date = contentDto.Date;
                content.Status = contentDto.Status;
                content.Type = contentDto.Type;

                if (contentDto.ThumbnailFile != null)
                {
                    string imageUrl = await _imageUploadService.UploadImageAsync(contentDto.ThumbnailFile);
                    if (string.IsNullOrEmpty(imageUrl))
                    {
                        response.Success = false;
                        response.Message = "Error al subir la nueva miniatura. No se actualizó la imagen.";
                        return response;
                    }

                    content.ThumbnailPath = imageUrl;
                }

                if (contentDto.MediaFile != null)
                {
                    string videoUrl = await _videoUploadService.UploadVideoAsync(contentDto.MediaFile);
                    if (string.IsNullOrEmpty(videoUrl))
                    {
                        response.Success = false;
                        response.Message = "Error al subir el nuevo video. No se actualizó el video.";
                        return response;
                    }

                    content.MediaPath = videoUrl;
                }


                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Playlist actualizada exitosamente.";
                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar la Contenido.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteContentAsync(string id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var content = await _context.Contents.FindAsync(Guid.Parse(id));
                if (content == null)
                {
                    response.Success = false;
                    response.Message = "Contenido no encontrado.";
                    response.Data = false;
                    return response;
                }

                _context.Contents.Remove(content);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Contenido eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el contenido.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<ContentDto>>> GetContentsByPlaylistIdAsync(string playlistId)
        {
            var response = new ServiceResponse<IEnumerable<ContentDto>>();
            try
            {
                var contents = await _context.Contents
                    .Where(c => c.PlaylistId.ToString() == playlistId)
                    .Select(c => new ContentDto
                    {
                        Id = c.Id,
                        TutorId = c.TutorId,
                        PlaylistId = c.PlaylistId,
                        Title = c.Title,
                        Description = c.Description,
                        MediaPath = c.MediaPath,
                        ThumbnailPath = c.ThumbnailPath,
                        Date = c.Date,
                        Status = c.Status,
                        Type = c.Type
                    }).ToListAsync();

                response.Data = contents;
                response.Message = contents.Any()
                    ? "Contenidos obtenidos correctamente."
                    : "No hay contenidos para esta playlist.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener contenidos por playlist.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<ContentStatsDto>> GetContentStatsAsync(string contentId)
        {
            var response = new ServiceResponse<ContentStatsDto>();
            try
            {
                var contentGuid = Guid.Parse(contentId);
                var totalLikes = await _context.Likes.CountAsync(l => l.ContentId == contentGuid);
                var totalComments = await _context.Comments.CountAsync(c => c.ContentId == contentGuid);

                response.Data = new ContentStatsDto
                {
                    TotalLikes = totalLikes,
                    TotalComments = totalComments
                };
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener estadísticas del contenido.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<CommentDto>>> GetCommentsByContentIdAsync(string contentId)
        {
            var response = new ServiceResponse<IEnumerable<CommentDto>>();
            try
            {
                var contentGuid = Guid.Parse(contentId);
                var comments = await _context.Comments
                    .Where(c => c.ContentId == contentGuid)
                    .Select(c => new CommentDto
                    {
                        Id = c.Id,
                        UserId = c.UserId,
                        ContentId = c.ContentId,
                        CommentText = c.CommentText,
                        Date = c.Date
                    }).ToListAsync();

                response.Data = comments;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los comentarios.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }


    }
}
