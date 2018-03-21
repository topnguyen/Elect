﻿using System.Threading.Tasks;

namespace Elect.Test.AspNetCore.Services
{
    // This class is used by the application to send email for account confirmation and password
    // reset. For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        public Task SendEmailAsync(string email, string subject, string message)
        {
            return Task.CompletedTask;
        }
    }
}