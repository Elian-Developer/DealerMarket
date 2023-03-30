using DealerMarket.Core.Application.Dtos.Email;
using DealerMarket.Core.Application.Interfaces.Services;
using DealerMarket.Core.Domain.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;

namespace DealerMarket.Infraestructure.Shared.Services
{
    public class EmailServices : IEmailService
    {
        private MailSettings _mailSettings;

        public EmailServices(IOptions<MailSettings> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

       public async Task SendAsync(EmailRequest request)
       {
            try
            {
                MimeMessage email = new();
                email.Sender = MailboxAddress.Parse($"{_mailSettings.DisplayName} < {_mailSettings.EmailFrom}>"); //Nombre y correo de quien lo esta enviando
                email.To.Add(MailboxAddress.Parse(request.To)); // A quien se está enviando el correo
                email.Subject = request.Subject; // El asunto del correo

                BodyBuilder builder = new(); //Allow build email body / me permite construir el cuerpo del correo
                builder.HtmlBody = request.Body;
                email.Body = builder.ToMessageBody();

                using SmtpClient smtp = new();    //configuring the smtp
                smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.Connect(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls); //Me conecto
                smtp.Authenticate(_mailSettings.SmtpUser, _mailSettings.SmtpPass); //Me autenctico
                await smtp.SendAsync(email); // envio el correo
                smtp.Disconnect(true); // Me desconecto
            }
            catch(Exception ex)
            {

            }
       }
    }
}
