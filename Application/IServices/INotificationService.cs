using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Application.IServices
{
    public interface INotificationService
    {
        Task SendEmailAsync(string to, string verificationcode);
        Task SendSms(string toPhoneNumber, string verificationcode);
    }
}
