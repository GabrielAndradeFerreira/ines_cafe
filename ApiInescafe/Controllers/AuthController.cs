using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ApiInescafe.DTOs.Auth;
using ApiInescafe.Services.Interfaces;

namespace ApiInescafe.Controller;
[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ITokenService _tokenService;
    public AuthController(IUserService userService, ITokenService tokenService)
    {
        _userService = userService;
        _tokenService = tokenService;
    }
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var user = await _userService.GetUserByUsernameAsync(loginDto.Username);
        if (string.IsNullOrEmpty(user?.Username))
        {
            return Unauthorized(new { message = "Usuário ou senha inválidos." });
        }
        
        var isPasswordValid = await _userService.ValidatePasswordAsync(user, loginDto.Password);
        if (!isPasswordValid)
        {
            return Unauthorized(new { message = "Usuário ou senha inválidos." });
        }

        var tokenString = _tokenService.GenerateToken(user);
        
        return Ok(new LoginResponseDto { Token = tokenString });
    }
    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        try
        {
            var newUser = await _userService.RegisterUserAsync(registerDto);

            return StatusCode(201, new { message = "Usuário registrado com sucesso." });
        }
        catch (ApplicationException ex)
        {

            return Conflict(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Ocorreu um erro interno.", error = ex.Message });
        }
    }
}