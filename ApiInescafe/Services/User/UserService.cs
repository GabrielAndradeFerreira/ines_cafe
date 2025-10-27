using ApiInescafe.Data;
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
