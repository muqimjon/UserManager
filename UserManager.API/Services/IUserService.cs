namespace UserManager.API.Services;

using UserManager.API.Models;

public interface IUserService
{
    Task RegisterAsync(RegisterRequest request);
    Task<User?> LoginAsync(LoginRequest request);
    IEnumerable<User> GetAll();
    Task BlockAsync(long id);
    Task UnblockAsync(long id);
    Task DeleteAsync(long id);
}
