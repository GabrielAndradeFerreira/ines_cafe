using System.ComponentModel.DataAnnotations;

namespace ApiInescafe.DTOs;
public class LoginDTO
{
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
    public required string PasswordHash { get; set; }
}
public class RegistroDTO
{
    [Required(ErrorMessage = "Nome é obrigatório")]
    [StringLength(100, ErrorMessage = "Nome deve ter no máximo 100 caracteres")]
    public required string Name { get; set; }
    
    [Required(ErrorMessage = "Email é obrigatório")]
    [EmailAddress(ErrorMessage = "Email inválido")]
    public required string Email { get; set; }
    
    [Required(ErrorMessage = "Senha é obrigatória")]
    [MinLength(6, ErrorMessage = "Senha deve ter no mínimo 6 caracteres")]
    public required string PasswordHash { get; set; }
}
public class AuthResponseDTO
{
    public required string Token { get; set; }
    public required string Email { get; set; }
    public required string Nome { get; set; }
    public DateTime Expiracao { get; set; }
}
