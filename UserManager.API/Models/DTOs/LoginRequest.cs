namespace UserManager.API.Models.DTOs;

using System.ComponentModel.DataAnnotations;

public class LoginRequest
{
    [EmailAddress, Required]
    public string Email { get; set; } = default!;
    [Required]
    public string Password { get; set; } = default!;
}
