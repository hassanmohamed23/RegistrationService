using Application.IServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {
        public readonly ILogger<NotificationService> _logger;
        private readonly IConfiguration _config;

        public NotificationService(IConfiguration config, ILogger<NotificationService> logger)
        {
            _config= config;
            _logger = logger;
        }

        public async Task SendEmailAsync(string to, string verificationcode)
        {
            MailMessage message = new MailMessage();

            var Message = _config["EmailSettings:Body"];
            message.From = new MailAddress(_config["EmailSettings:From"]);
            message.To.Add(new MailAddress(to));
            message.Subject = _config["EmailSettings:Subject"];
            message.Body = Message.Replace("@Code", verificationcode);

            message.IsBodyHtml = true;

            try
            {
                using var client = new SmtpClient
                {
                    Host = _config["EmailSettings:Host"],
                    EnableSsl = bool.Parse(_config["EmailSettings:EnableSsl"]),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
              
                };

                await client.SendMailAsync(message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
        }

        public async Task SendSms(string toPhoneNumber, string verificationcode)
        {
            try
            {
                var accountSid = _config["Twilio:AccountSid"];
                var authToken = _config["Twilio:AuthToken"];
                var twilioPhoneNumber = _config["Twilio:PhoneNumber"];
                var Message = _config["Twilio:Message"];

                TwilioClient.Init(accountSid, authToken);
                var messageResponse =await MessageResource.CreateAsync(
                    body: Message.Replace("@Code", verificationcode),
                    from: new PhoneNumber(twilioPhoneNumber),
                    to: new PhoneNumber(toPhoneNumber)
                );

                Console.WriteLine($"SMS Sent: SID: {messageResponse.Sid}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error sending SMS: {ex.Message}");
            }
        }
    }
}
