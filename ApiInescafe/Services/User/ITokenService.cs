using ApiInescafe.Models;

namespace ApiInescafe.Services.Interfaces;
public interface ITokenService
{
    string GenerateToken(UserModel user);
}
