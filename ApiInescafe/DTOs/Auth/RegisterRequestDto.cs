using System.ComponentModel.DataAnnotations;

namespace ApiInescafe.DTOs.Auth;
public class RegisterRequestDto
{
    [Required]
    [MinLength(3)]
    public string Username { get; set; } = string.Empty;
    [Required]
    [MinLength(6)]
    public string Password { get; set; } = string.Empty;
}