using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;
using UniversityRegistrar.Models.Entities;

namespace UniversityRegistrar.Utilities
{
    public static class EmailComposer
    {
        private static readonly IConfigurationRoot _configuration = new ConfigurationBuilder()
              .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
              .AddJsonFile("appsettings.json")
              .Build();

        public static void Send (string receiverEmail, string emailSubject, string emailBody)
        {
            var senderEmail = new MailAddress(_configuration.GetSection("Smtp")["Email"], "Org");

            var EmailReceiver = new MailAddress(receiverEmail, "Пользователь");

            string _password = _configuration.GetSection("Smtp")["Password"];
            string _sub = emailSubject;
            string _body = emailBody;

            var smtp = new SmtpClient
            {
                Host = _configuration.GetSection("Smtp")["Host"],
                Port = int.Parse(_configuration.GetSection("Smtp")["Port"]),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, _password)
            };

            using (var message = new MailMessage(senderEmail, EmailReceiver))
            {
                message.Subject = _sub;
                message.Body = _body;
                message.IsBodyHtml = true;
                ServicePointManager.ServerCertificateValidationCallback += (s, cert, chain, sslPolicyErrors) => true;
                smtp.Send(message);
            }

            smtp.Dispose();
        }
    }
}
