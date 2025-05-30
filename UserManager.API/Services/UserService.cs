namespace UserManager.API.Services;

using UserManager.API.Data;
using UserManager.API.Models;
using UserManager.API.Models.DTOs;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task<int> RegisterAsync(RegisterRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
        await repository.AddAsync(user);
        return await repository.SaveAsync();
    }

    public async Task<User?> LoginAsync(LoginRequest request)
    {
        var user = await repository.GetByEmailAsync(request.Email);
        if (user is null || user.IsBlocked) return null;
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return null;
        user.LastLoginAt = DateTimeOffset.UtcNow;
        repository.Update(user);
        await repository.SaveAsync();
        return user;
    }

    public IEnumerable<User> GetAll()
        => repository.GetAll()
                     .OrderByDescending(u => u.LastLoginAt);

    public async Task<int> BlockAsync(long id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user is null)
            return default;
        user.IsBlocked = true;
        repository.Update(user);
        return await repository.SaveAsync();
    }

    public async Task<int> UnblockAsync(long id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user is null)
            return default;
        user.IsBlocked = false;
        repository.Update(user);
        return await repository.SaveAsync();
    }

    public async Task<int> DeleteAsync(long id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user is null)
            return default;
        repository.Delete(user);
        return await repository.SaveAsync();
    }
}
