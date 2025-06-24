using Carlitos5G.Commons;
using Carlitos5G.Models;

namespace Carlitos5G.Services
{
    public interface IAuthService 
    {
        Task<ServiceResponse<string>> AuthenticateAsync(string email, string password);

    }
}
