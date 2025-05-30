namespace UserManager.API.Data;

using Microsoft.EntityFrameworkCore;
using UserManager.API.Models;

public class UserRepository(AppDbContext context) : IUserRepository
{
    public async Task AddAsync(User user)
    {
        user.RegisteredAt = DateTime.UtcNow;
        await context.Users.AddAsync(user);
    }

    public void Delete(User user)
        => context.Users.Remove(user);

    public IEnumerable<User> GetAll()
        => context.Users.AsNoTracking();

    public Task<User?> GetByEmailAsync(string email)
        => context.Users.FirstOrDefaultAsync(user => user.Email.Equals(email));

    public async Task<User?> GetByIdAsync(long id)
        => await context.Users.FindAsync(id);

    public async Task<int> SaveAsync()
        => await context.SaveChangesAsync();

    public void Update(User user)
        => context.Users.Update(user);
}
