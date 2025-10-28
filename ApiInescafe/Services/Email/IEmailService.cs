namespace ApiInescafe.Services.Email;

public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string htmlContent);
}