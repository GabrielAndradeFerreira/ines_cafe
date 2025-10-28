using SendGrid;
using SendGrid.Helpers.Mail;
namespace ApiInescafe.Services.Email;

public class SendGridEmailService : IEmailService
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<SendGridEmailService> _logger;

    public SendGridEmailService(IConfiguration configuration, ILogger<SendGridEmailService> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string htmlContent)
    {
        // Busca a API Key do User Secrets (ou variáveis de ambiente na produção)
        var apiKey = _configuration["SendGrid:ApiKey"]; 
        
        // Busca o email/nome do appsettings.json
        var fromEmail = _configuration["SendGrid:FromEmail"];
        var fromName = _configuration["SendGrid:FromName"];

        if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(fromEmail))
        {
            _logger.LogError("Configurações do SendGrid (ApiKey ou FromEmail) não encontradas.");
            return;
        }

        var client = new SendGridClient(apiKey);
        var msg = new SendGridMessage()
        {
            From = new EmailAddress(fromEmail, fromName),
            Subject = subject,
            HtmlContent = htmlContent
        };
        msg.AddTo(new EmailAddress(toEmail));

        var response = await client.SendEmailAsync(msg);

        if (response.IsSuccessStatusCode)
        {
            _logger.LogInformation($"Email para {toEmail} enviado com sucesso.");
        }
        else
        {
            var errorBody = await response.Body.ReadAsStringAsync();
            _logger.LogError($"Falha ao enviar email para {toEmail}. Status: {response.StatusCode}. Response: {errorBody}");
        }
    }
}