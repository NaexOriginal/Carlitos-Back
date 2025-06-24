using Carlitos5G.Commons;
using Carlitos5G.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carlitos5G.Services
{
    public interface IUserService
    {
        Task<ServiceResponse<IEnumerable<UserDto>>> GetAllUsersAsync();

        Task<ServiceResponse<UserDto>> GetUserByIdAsync(string id);

        Task<ServiceResponse<UserDto>> CreateUserAsync(UserDto userDto);

        Task<ServiceResponse<UserDto>> UpdateUserAsync(string id, UserDto userDto);

        Task<ServiceResponse<bool>> DeleteUserAsync(string id);
        
        Task<ServiceResponse<UserDto>> FindUserByIdOrEmailAsync(string searchTerm);
        
        Task<ServiceResponse<UserActivityDto>> GetUserActivityAsync(string userId);
        
        Task<ServiceResponse<bool>> DeleteUserWithRelatedDataAsync(string userId);
        
        Task<ServiceResponse<StudentCoursesDto>> GetStudentCoursesWithProgressAsync(string userId);

        Task<ServiceResponse<List<AvailableCourseDto>>> GetAvailableCoursesForStudentAsync(string userId);

    }
}
