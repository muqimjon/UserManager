namespace UserManager.API.Data;

using UserManager.API.Models;

public interface IUserRepository
{
    IEnumerable<User> GetAll();
    Task<User?> GetByIdAsync(long id);
    Task<User?> GetByEmailAsync(string email);
    Task AddAsync(User user);
    void Update(User user);
    void Delete(User user);
    Task<int> SaveAsync();
}
