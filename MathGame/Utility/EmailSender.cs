using Microsoft.AspNetCore.Identity.UI.Services;

namespace MathGame.Utility
{
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            //login to send email
            return Task.CompletedTask;
        }
    }
}
