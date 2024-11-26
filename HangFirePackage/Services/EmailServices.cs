using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using HangFirePackage.DTOs;

namespace HangFirePackage.Services;

public class EmailServices: IEmailServices
{
    private readonly IConfiguration config;

    public EmailServices(IConfiguration config)
    {
        this.config = config;
    }
    public void SendEmail(EmailDto dto)
    {
        MimeMessage email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(config["EmailSender:Email"]));
        email.To.Add(MailboxAddress.Parse(dto.To));
        email.Subject = "Report of E-Commerce";
        email.Body = new TextPart(TextFormat.Html) { Text = dto.Body };

        using var smtp = new SmtpClient();
        smtp.Connect(config["EmailSender:EmailHost"], 587, SecureSocketOptions.StartTls);
        smtp.Authenticate(config["EmailSender:Email"], config["EmailSender:Password"]);

        smtp.Send(email);
        smtp.Disconnect(true);
    }
}
