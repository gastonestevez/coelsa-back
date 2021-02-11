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
using System.Web.Http.Description;

namespace COELSAapi.Controllers
{
    [RoutePrefix("api/email")]
    public class EmailController : ApiController
    {
        /// <summary>
        /// Envia un e-mail
        /// </summary>
        /// <param name="email">Email entity</param>        
        [HttpPost]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> SendEmail(Email email)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var message = new MailMessage();
                message.To.Add(new MailAddress(UtilCodes.DestinatarioEmail));
                message.From = new MailAddress(email.EmailContact);
                message.Subject = email.AsuntoDescripcion;
                message.Body = CreateEmailBody(email);
                message.IsBodyHtml = true;
                using (var smtp = new SmtpClient())
                {
                    await smtp.SendMailAsync(message);
                    await Task.FromResult(0);
                }

                return Ok();
            }
            catch (Exception ex)
            {
                //log
                return StatusCode(HttpStatusCode.InternalServerError);
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
