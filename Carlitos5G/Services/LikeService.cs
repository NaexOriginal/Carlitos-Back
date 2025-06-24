using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Carlitos5G.Commons;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Services
{
    public class LikeService : ILikeService
    {
        private readonly ApplicationDbContext _context;

        public LikeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<LikeDto>>> GetAllLikesAsync()
        {
            var response = new ServiceResponse<IEnumerable<LikeDto>>();
            try
            {
                var likes = await _context.Likes
                    .Select(l => new LikeDto
                    {
                        UserId = l.UserId,
                        TutorId = l.TutorId,
                        ContentId = l.ContentId
                    })
                    .ToListAsync();

                response.Data = likes;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los 'likes'.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<LikeDto>> GetLikeByIdAsync(string userId, string tutorId, string? contentId)
        {
            var response = new ServiceResponse<LikeDto>();
            try
            {
                var like = await _context.Likes
                    .Where(l => l.UserId.ToString() == userId && l.TutorId.ToString() == tutorId && l.ContentId.ToString() == contentId)
                    .Select(l => new LikeDto
                    {
                        UserId = l.UserId,
                        TutorId = l.TutorId,
                        ContentId = l.ContentId
                    })
                    .FirstOrDefaultAsync();

                if (like == null)
                {
                    response.Success = false;
                    response.Message = "Like no encontrado.";
                }
                else
                {
                    response.Data = like;
                }
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el like.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<LikeDto>> CreateLikeAsync(LikeDto likeDto)
        {
            var response = new ServiceResponse<LikeDto>();
            try
            {
                var like = new Like
                {
                    UserId = likeDto.UserId,
                    TutorId = likeDto.TutorId,
                    ContentId = likeDto.ContentId
                };

                _context.Likes.Add(like);
                await _context.SaveChangesAsync();

                response.Data = likeDto;
                response.Message = "Like creado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el like.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteLikeAsync(string userId, string tutorId, string? contentId)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var like = await _context.Likes
                    .FirstOrDefaultAsync(l => l.UserId.ToString() == userId && l.TutorId.ToString() == tutorId && l.ContentId.ToString() == contentId);

                if (like == null)
                {
                    response.Success = false;
                    response.Message = "Like no encontrado.";
                    response.Data = false;
                    return response;
                }

                _context.Likes.Remove(like);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Like eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el like.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<LikeDto>>> GetLikesByUserIdAsync(string userId)
        {
            var response = new ServiceResponse<IEnumerable<LikeDto>>();
            try
            {
                if (!Guid.TryParse(userId, out Guid userGuid))
                {
                    response.Success = false;
                    response.Message = "ID de usuario inválido.";
                    return response;
                }

                var likes = await _context.Likes
                    .Where(l => l.UserId == userGuid)
                    .Select(l => new LikeDto
                    {
                        UserId = l.UserId,
                        TutorId = l.TutorId,
                        ContentId = l.ContentId
                    })
                    .ToListAsync();

                response.Data = likes;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los likes por usuario.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<IEnumerable<LikeDto>>> GetLikesByContentIdAsync(string contentId)
        {
            var response = new ServiceResponse<IEnumerable<LikeDto>>();
            try
            {
                if (!Guid.TryParse(contentId, out Guid contentGuid))
                {
                    response.Success = false;
                    response.Message = "ID de contenido inválido.";
                    return response;
                }

                var likes = await _context.Likes
                    .Where(l => l.ContentId == contentGuid)
                    .Select(l => new LikeDto
                    {
                        UserId = l.UserId,
                        TutorId = l.TutorId,
                        ContentId = l.ContentId
                    })
                    .ToListAsync();

                response.Data = likes;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener los likes por contenido.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<int>> GetLikesCountByTutorIdAsync(string tutorId)
        {
            var response = new ServiceResponse<int>();
            try
            {
                if (!Guid.TryParse(tutorId, out Guid tutorGuid))
                {
                    response.Success = false;
                    response.Message = "ID de tutor inválido.";
                    return response;
                }

                var count = await _context.Likes
                    .CountAsync(l => l.TutorId == tutorGuid);

                response.Data = count;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el conteo de likes por tutor.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<int>> GetLikesCountByContentIdAsync(string contentId)
        {
            var response = new ServiceResponse<int>();
            try
            {
                if (!Guid.TryParse(contentId, out Guid contentGuid))
                {
                    response.Success = false;
                    response.Message = "ID de contenido inválido.";
                    return response;
                }

                var count = await _context.Likes
                    .CountAsync(l => l.ContentId == contentGuid);

                response.Data = count;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el conteo de likes por contenido.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }
    }
}
