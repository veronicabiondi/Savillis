using System;
using System.Threading.Tasks;

namespace Savillis.WebAPI.Infrastructure
{
    public interface IEmailService
    {
        Task SendMail(string email);
    }

    public class EmailService : IEmailService
    {
        public async Task SendMail(string email)
        {
            //Integration with SendGrid
            await Task.Delay(TimeSpan.Zero);
        }
    }
}