using System.Threading.Tasks;

namespace Elect.Test.AspNetCore.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
    }
}