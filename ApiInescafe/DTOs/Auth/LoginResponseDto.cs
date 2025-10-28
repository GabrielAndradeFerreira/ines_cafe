namespace ApiInescafe.DTOs.Auth;
// Resposta enviada ao cliente após o login
public class LoginResponseDto
{
    public string Token { get; set; } = string.Empty;
}