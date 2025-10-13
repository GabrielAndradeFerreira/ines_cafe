using System.ComponentModel.DataAnnotations;
namespace ApiInescafe.Models;

public class User
{
    public required string Id { get; set; }

    public required string Name { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(150)]
    public required string Email { get; set; }
    public required string PasswordHash { get; set; }
    public string? ImageUrl { get; set; }
    public bool IsAdmin { get; set; }
}