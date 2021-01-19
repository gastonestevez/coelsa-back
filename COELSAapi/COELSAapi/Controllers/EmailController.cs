using COELSAapi.Models;
using COELSAapi.Models.Utils;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace COELSAapi.Controllers
{
    [RoutePrefix("api/email")]
    public class EmailController : ApiController
    {
        [HttpPost]
        public async Task SendEmail(Email email)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(UtilCodes.DestinatarioEmail));
            message.From = new MailAddress(email.EmailContact);
            message.Subject = UtilCodes.GetAsuntoEmail(email.Asunto);
            message.Body = CreateEmailBody(email);
            message.IsBodyHtml = true;
            using (var smtp = new SmtpClient())
            {                
                await smtp.SendMailAsync(message);
                await Task.FromResult(0);
            }
        }

        private string CreateEmailBody(Email email)
        {
            var body = string.Empty;
            body = $"{email.Mensaje} <br><br>{email.Nombre} {email.Apellido} <br> {email.Telefono} <br> {email.EmailContact}";
            return body;
        }
    }
}
