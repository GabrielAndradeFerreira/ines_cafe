using ApiInescafe.DTOs.Auth;
using ApiInescafe.Models;

namespace ApiInescafe.Services.Interfaces;
public interface IUserService
{
    Task<UserModel?> GetUserByUsernameAsync(string username);
    Task<UserModel> RegisterUserAsync(RegisterRequestDto registerDto);
    Task<bool> ValidatePasswordAsync(UserModel user, string password);
}
