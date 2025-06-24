using Carlitos5G.Commons;
using Carlitos5G.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carlitos5G.Services
{
    public interface ILikeService
    {
        Task<ServiceResponse<IEnumerable<LikeDto>>> GetAllLikesAsync();
        Task<ServiceResponse<LikeDto>> GetLikeByIdAsync(string userId, string tutorId, string? contentId);
        Task<ServiceResponse<LikeDto>> CreateLikeAsync(LikeDto likeDto);
        Task<ServiceResponse<bool>> DeleteLikeAsync(string userId, string tutorId, string? contentId);
        Task<ServiceResponse<IEnumerable<LikeDto>>> GetLikesByUserIdAsync(string userId);
        Task<ServiceResponse<IEnumerable<LikeDto>>> GetLikesByContentIdAsync(string contentId);
        Task<ServiceResponse<int>> GetLikesCountByTutorIdAsync(string tutorId);
        Task<ServiceResponse<int>> GetLikesCountByContentIdAsync(string contentId);
    }
}
