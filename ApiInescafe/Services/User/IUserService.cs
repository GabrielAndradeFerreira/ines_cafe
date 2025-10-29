using ApiInescafe.DTOs;
using ApiInescafe.DTOs.Auth;
using ApiInescafe.Models;

namespace ApiInescafe.Services.Interfaces;
public interface IUserService
{
    Task<UserModel?> GetUserByUsernameAsync(string username);
    Task<UserModel> RegisterUserAsync(RegisterRequestDto registerDto);
    Task<bool> ValidatePasswordAsync(UserModel user, string password);
    Task<ResponseModel<bool>> AddPersonalInformations(PersonalInfoDto personalInfoDto, int userId);
    Task<ResponseModel<bool>> AddAddressInformations(AddressInfoDto addressInfoDto, int userId);
}
