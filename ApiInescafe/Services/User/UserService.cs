using ApiInescafe.Data;
using ApiInescafe.DTOs;
using ApiInescafe.DTOs.Auth;
using ApiInescafe.Models;
using ApiInescafe.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiInescafe.Services;
public class UserService : IUserService
{
    private readonly AppDbContext _context;
    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<bool>> AddAddressInformations(AddressInfoDto addressInfoDto, int userId)
    {
        var response = new ResponseModel<bool>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                response.Message = "Usuário não encontrado.";
                response.Status = false;
                return response;
            }
            user.City = addressInfoDto.City;
            user.State = addressInfoDto.State;
            user.Street = addressInfoDto.Street;
            user.ZipCode = addressInfoDto.ZipCode;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            response.Status = true;
            return response;
        }
        catch(Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<ResponseModel<bool>> AddPersonalInformations(PersonalInfoDto personalInfoDto, int userId)
    {
        var response = new ResponseModel<bool>();
        try
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                response.Message = "Usuário não encontrado.";
                response.Status = false;
                return response;
            }
            user.CompleteName = personalInfoDto.CompleteName;
            user.Email = personalInfoDto.Email;
            user.Document = personalInfoDto.Document;
            user.Phone = personalInfoDto.Phone;
            user.BirthDate = personalInfoDto.BirthDate;            
            user.Gender = personalInfoDto.Gender;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            response.Status = true;
            return response;
        }
        catch(Exception e)
        {
            response.Message = e.Message;
            response.Status = false;
            return response;
        }
    }

    public async Task<UserModel?> GetUserByUsernameAsync(string username)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username.ToLower() == username.ToLower());
    }
    public async Task<UserModel> RegisterUserAsync(RegisterRequestDto registerDto)
    {
        bool userExists = await _context.Users
            .AnyAsync(u => u.Username.ToLower() == registerDto.Username.ToLower());
        if (userExists)
        {
            throw new ApplicationException("Nome de usuário já existe.");
        }
        string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password);
        var newUser = new UserModel
        {
            Username = registerDto.Username,
            PasswordHash = passwordHash,
        };

        await _context.Users.AddAsync(newUser);
        await _context.SaveChangesAsync();
        return newUser;
    }

    public Task<bool> ValidatePasswordAsync(UserModel user, string password)
    {
        bool isValid = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);
        return Task.FromResult(isValid);
    }
}
