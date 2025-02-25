using SynergyAccounts.DTOS.AuthDto;
using SynergyAccounts.Models;

namespace SynergyAccounts.Interface
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterDto registerDto);
        Task<User> LoginAsync(string email, string password);
    }
}
