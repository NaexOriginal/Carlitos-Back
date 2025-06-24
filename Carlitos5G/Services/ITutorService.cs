using Carlitos5G.Commons;
using Carlitos5G.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Carlitos5G.Services
{
    public interface ITutorService
    {
        Task<ServiceResponse<IEnumerable<TutorDto>>> GetAllTutorsAsync();

        Task<ServiceResponse<TutorDto>> GetTutorByIdAsync(string id);

        Task<ServiceResponse<TutorDto>> CreateTutorAsync(TutorDto tutorDto);

        Task<ServiceResponse<TutorDto>> UpdateTutorAsync(string id, TutorDto tutorDto);

        Task<ServiceResponse<bool>> DeleteTutorAsync(string id);

        Task<ServiceResponse<IEnumerable<TutorDto>>> SearchTutorsAsync(string searchTerm);

        Task<ServiceResponse<TutorStatsDto>> GetTutorStatsAsync(string id);

        Task<ServiceResponse<IEnumerable<PlaylistDto>>> GetTutorCoursesAsync(string tutorId);
    }
}
