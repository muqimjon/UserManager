namespace UserManager.API.Models;

public class User
{
    public long Id { get; set; }

    public string Name { get; set; } = default!;

    public string Email { get; set; } = default!;

    public string PasswordHash { get; set; } = default!;

    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }

    public bool IsBlocked { get; set; } = false;
}
