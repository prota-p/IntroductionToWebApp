using Microsoft.AspNetCore.Identity;
using System.Net.Mail;
using System.Net;

namespace BlazorMailApp.Data.EmailSender
{
    public class CustomEmailSender : IEmailSender<ApplicationUser>
    {
        public async Task SendConfirmationLinkAsync(ApplicationUser user, string email, string confirmationLink)
        {
            string fromEmail = Environment.GetEnvironmentVariable("GMAIL_ADDRESS");
            string fromUserName = "メール自動送信システム";
            var fromAddress = new MailAddress(fromEmail, fromUserName);
            string fromPassword = Environment.GetEnvironmentVariable("GMAIL_APP_PASSWORD");

            string toEmail = email; //★(a)送信先メールアドレス
            string toUserName = user.UserName; //★(b)送信先ユーザー名
            var toAddress = new MailAddress(toEmail, toUserName);

            string subject = "アカウント確認のリンク";
            //★(c)HTMLエンコードされたリンクをデコードする
            string decodedLink = System.Net.WebUtility.HtmlDecode(confirmationLink); 
            //★(d)メール本文へのリンクの埋め込み
            string body = $"以下のリンクをクリックしてアカウントを確認してください： {decodedLink}";

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

        public Task SendPasswordResetLinkAsync(ApplicationUser user, string email, string resetLink)
        {
            //Todo : パスワードリセット用のリンクを送信する
            throw new NotImplementedException();
        }


        public Task SendPasswordResetCodeAsync(ApplicationUser user, string email, string resetCode)
        {
            //Todo : パスワードリセット用のコードを送信する
            throw new NotImplementedException();
        }
    }
}
