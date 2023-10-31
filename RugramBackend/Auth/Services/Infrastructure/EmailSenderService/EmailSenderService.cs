using System.Net;
using System.Net.Mail;

namespace Auth.Services.Infrastructure.EmailSenderService;

public class EmailSenderService : IEmailSenderService
{
    private readonly IConfiguration _configuration;

    public EmailSenderService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendMessageAsync(string subject, string body, string sendTo)
    {
        var from = new MailAddress(_configuration["EmailConfig:Sender"]!,
            _configuration["EmailConfig:SenderName"]!);
        var to = new MailAddress(sendTo);
        var message = new MailMessage(from, to)
        {
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };

        var smtp = new SmtpClient(_configuration["SmtpSettings:SmtpAddress"],
            int.Parse(_configuration["SmtpSettings:Port"]!))
        {
            Credentials = new NetworkCredential(_configuration["EmailConfig:Sender"]!,
                _configuration["EmailConfig:SenderPassword"]!),
            EnableSsl = true,
            UseDefaultCredentials = false,
        };

        await smtp.SendMailAsync(message);
    }
}