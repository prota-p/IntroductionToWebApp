using System.Net.Mail;
using System.Net;
class Program
{
    static async Task Main(string[] args)
    {
        string fromEmail = Environment.GetEnvironmentVariable("GMAIL_ADDRESS");
        string fromUserName = "メール自動送信システム";
        var fromAddress = new MailAddress(fromEmail, fromUserName);
        string fromPassword = Environment.GetEnvironmentVariable("GMAIL_APP_PASSWORD");

        string toEmail = "destination@example.com"; // ★送信先メールアドレス
        string toUserName = "Destination User"; // ★送信先ユーザー名
        var toAddress = new MailAddress(toEmail, toUserName);

        string subject = "テストメール";
        string body = "これはテストメールです。";

        using (var message = new MailMessage(fromAddress, toAddress)
        {
            Subject = subject,
            Body = body
        })
        {
            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
            };
            await smtp.SendMailAsync(message);
        }
    }
}