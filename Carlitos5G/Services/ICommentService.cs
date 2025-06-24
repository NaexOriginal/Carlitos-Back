using Carlitos5G.Commons;
using Carlitos5G.Dtos;

namespace Carlitos5G.Services
{
    public interface ICommentService
    {
        Task<ServiceResponse<IEnumerable<CommentDto>>> GetAllCommentsAsync();
        Task<ServiceResponse<CommentDto>> GetCommentByIdAsync(string id);
        Task<ServiceResponse<CommentDto>> CreateCommentAsync(CommentDto commentDto);
        Task<ServiceResponse<CommentDto>> UpdateCommentAsync(CommentDto commentDto);
        Task<ServiceResponse<bool>> DeleteCommentAsync(string id);
    }
}
