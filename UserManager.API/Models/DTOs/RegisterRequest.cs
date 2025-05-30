namespace UserManager.API.Models.DTOs;

using System.ComponentModel.DataAnnotations;

public class RegisterRequest
{
    public string Name { get; set; } = string.Empty;
    [EmailAddress, Required]
    public string Email { get; set; } = string.Empty;
    [Required]
    public string Password { get; set; } = string.Empty;
}
