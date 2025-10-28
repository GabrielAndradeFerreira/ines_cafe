using ApiInescafe.DTOs.Email;
using ApiInescafe.Services.Email;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailService;

    // O IEmailService é injetado aqui
    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendEmail([FromBody] EmailRequestDto request)
    {
        try
        {
            await _emailService.SendEmailAsync(request.To, request.Subject, request.Body);
            return Ok(new { message = "Email enviado com sucesso." });
        }
        catch (Exception)
        {
            // Logar a exceção
            return StatusCode(500, new { message = "Ocorreu um erro interno ao enviar o e-mail." });
        }
    }
}