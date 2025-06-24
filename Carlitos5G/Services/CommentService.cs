using Carlitos5G.Data;
using Carlitos5G.Dtos;
using Carlitos5G.Models;
using Carlitos5G.Commons;
using Microsoft.EntityFrameworkCore;

namespace Carlitos5G.Services
{
    public class CommentService : ICommentService
    {
        private readonly ApplicationDbContext _context;

        public CommentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<IEnumerable<CommentDto>>> GetAllCommentsAsync()
        {
            var response = new ServiceResponse<IEnumerable<CommentDto>>();
            try
            {
                var comments = await _context.Comments
                    .Select(c => new CommentDto
                    {
                        Id = c.Id,
                        ContentId = c.ContentId,
                        UserId = c.UserId,
                        TutorId = c.TutorId,
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

        public async Task<ServiceResponse<CommentDto>> GetCommentByIdAsync(string id)
        {
            var response = new ServiceResponse<CommentDto>();
            try
            {
                var comment = await _context.Comments
                    .Where(c => c.Id.ToString() == id)
                    .Select(c => new CommentDto
                    {
                        Id = c.Id,
                        ContentId = c.ContentId,
                        UserId = c.UserId,
                        TutorId = c.TutorId,
                        CommentText = c.CommentText,
                        Date = c.Date
                    }).FirstOrDefaultAsync();

                if (comment == null)
                {
                    response.Success = false;
                    response.Message = "Comentario no encontrado.";
                    return response;
                }

                response.Data = comment;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al obtener el comentario.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<CommentDto>> CreateCommentAsync(CommentDto commentDto)
        {
            var response = new ServiceResponse<CommentDto>();
            try
            {
                var comment = new Comment
                {
                    Id = commentDto.Id,
                    ContentId = commentDto.ContentId,
                    UserId = commentDto.UserId,
                    TutorId = commentDto.TutorId,
                    CommentText = commentDto.CommentText,
                    Date = commentDto.Date
                };

                _context.Comments.Add(comment);
                await _context.SaveChangesAsync();

                response.Data = commentDto;
                response.Message = "Comentario creado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al crear el comentario.";
                response.ErrorDetails = ex.Message;
            }
            return response;
        }

        public async Task<ServiceResponse<CommentDto>> UpdateCommentAsync(CommentDto commentDto)
        {
            var response = new ServiceResponse<CommentDto>();
            try
            {
                var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id == commentDto.Id);
                if (comment == null)
                {
                    response.Success = false;
                    response.Message = "Comentario no encontrado.";
                    return response;
                }

                comment.ContentId = commentDto.ContentId;
                comment.UserId = commentDto.UserId;
                comment.TutorId = commentDto.TutorId;
                comment.CommentText = commentDto.CommentText;
                comment.Date = commentDto.Date;

                await _context.SaveChangesAsync();
                response.Data = commentDto;
                response.Message = "Comentario actualizado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al actualizar el comentario.";
                response.ErrorDetails = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<bool>> DeleteCommentAsync(string id)
        {
            var response = new ServiceResponse<bool>();
            try
            {
                var comment = await _context.Comments.FirstOrDefaultAsync(c => c.Id.ToString() == id);
                if (comment == null)
                {
                    response.Success = false;
                    response.Message = "Comentario no encontrado.";
                    response.Data = false;
                    return response;
                }

                _context.Comments.Remove(comment);
                await _context.SaveChangesAsync();

                response.Data = true;
                response.Message = "Comentario eliminado exitosamente.";
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Error al eliminar el comentario.";
                response.ErrorDetails = ex.Message;
                response.Data = false;
            }
            return response;
        }
    }
}
