namespace UserManager.API.Services;

using UserManager.API.Models;
using UserManager.API.Models.DTOs;

public interface IUserService
{
    Task<int> RegisterAsync(RegisterRequest request);
    Task<User?> LoginAsync(LoginRequest request);
    IEnumerable<User> GetAll();
    Task<int> BlockAsync(long id);
    Task<int> UnblockAsync(long id);
    Task<int> DeleteAsync(long id);
}
