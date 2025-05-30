namespace UserManager.API.Services;

using UserManager.API.Data;
using UserManager.API.Models;

public class UserService(IUserRepository repository) : IUserService
{
    public async Task RegisterAsync(RegisterRequest request)
    {
        var user = new User
        {
            Name = request.Name,
            Email = request.Email,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };
        await repository.AddAsync(user);
        await repository.SaveAsync();
    }

    public async Task<User?> LoginAsync(LoginRequest request)
    {
        var user = await repository.GetByEmailAsync(request.Email);
        if (user is null) return null;
        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) return null;
        user.LastLoginAt = DateTimeOffset.UtcNow;
        repository.Update(user);
        await repository.SaveAsync();
        return user;
    }

    public IEnumerable<User> GetAll()
        => repository.GetAll()
                     .OrderByDescending(u => u.LastLoginAt);

    public async Task BlockAsync(long id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user is not null)
        {
            user.IsBlocked = true;
            repository.Update(user);
            await repository.SaveAsync();
        }
    }

    public async Task UnblockAsync(long id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user is not null)
        {
            user.IsBlocked = false;
            repository.Update(user);
            await repository.SaveAsync();
        }
    }

    public async Task DeleteAsync(long id)
    {
        var user = await repository.GetByIdAsync(id);
        if (user is not null)
        {
            repository.Delete(user);
            await repository.SaveAsync();
        }
    }
}
