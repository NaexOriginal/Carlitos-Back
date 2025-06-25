using Carlitos5G.Commons;
using Carlitos5G.Models;

namespace Carlitos5G.Services
{
    public interface IAdminServices
    {
        Task<ServiceResponse<IEnumerable<Admin>>> GetAllAdminsAsync();
        Task<ServiceResponse<Admin>> GetAdminByIdAsync(Guid id);
        Task<ServiceResponse<Admin>> CreateAdminAsync(Admin admin);
        Task<ServiceResponse<Admin>> UpdateAdminAsync(Admin admin);
        Task<ServiceResponse<bool>> DeleteAdminAsync(Guid id);
    }
}
