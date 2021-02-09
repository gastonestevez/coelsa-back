using COELSAapi.Models;
using COELSAapi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace COELSAapi.Controllers
{
    /// <summary>
    /// login controller class for authenticate users
    /// </summary>
    [AllowAnonymous]
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        private COELSADB_APIEntities db = new COELSADB_APIEntities();

        // POST: api/Login
        [HttpPost]
        [AllowAnonymous]
        [Route("Login")]
        public async Task<IHttpActionResult> LoginAsync(LoginRequest usuarioLogin)
        {
            if (usuarioLogin == null)
                return BadRequest("Usuario y Contraseña requeridos.");

            var _userInfo = await AutenticarUsuarioAsync(usuarioLogin.Email, usuarioLogin.Password);
            if (_userInfo != null)
            {
                return Ok(new { token = TokenGenerator.GenerarTokenJWT(_userInfo) });
            }
            else
            {
                return Unauthorized();
            }
        }

        private async Task<User> AutenticarUsuarioAsync(string email, string password)
        {
            return db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        private async Task<User> GetUserByEmail(string email)
        {
            return db.Users.FirstOrDefault(u => u.Email == email);
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(UserData userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User(userData.Name, userData.Email, userData.Password, userData.Role, DateTime.Now, userData.Company);

            var userDB = await GetUserByEmail(userData.Email);
            if (userDB != null)
            {
                return BadRequest("El e-mail ingresado ya existe");
            }

            db.Users.Add(user);
            db.SaveChanges();

            return Ok(user);

        }

        [HttpPost]
        [Route("ForgotPassword")]
        public async Task<IHttpActionResult> ForgotPassword(ForgotPassword userData)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var userDB = await GetUserByEmail(userData.Email);

                if (userDB == null)
                {
                    return BadRequest("El usuario no esta registrado");
                }

                var code = TokenGenerator.GenerarTokenJWT(userDB);
                var encodedCode = HttpUtility.UrlEncode(code);
               
                var message = new MailMessage();
                message.To.Add(new MailAddress(userDB.Email));
                message.Subject = "Coelsa - Reset Password";
                message.Body = /*Request.RequestUri.GetLeftPart(UriPartial.Authority) + "/api/ResetPassword*/UtilCodes.ClienteWeb+"/ResetPassword.html?Code=" + encodedCode;
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
                return StatusCode(HttpStatusCode.InternalServerError);
            }
            

        }

        [HttpPost]
        [Route("ResetPassword")]
        public async Task<IHttpActionResult> ResetPassword(ResetPassword model)
        {
            try
            {
                var userDB = db.Users.Where(u => u.Email == model.Email).FirstOrDefault();
                if (userDB == null)
                {
                    return Ok();
                }
                if(model.Password != model.ConfirmPassword)
                {
                    return BadRequest("Las contraseñas con coinciden.");
                }

                userDB.Password = model.Password;
                db.Entry(userDB).State = EntityState.Modified;

               
                await db.SaveChangesAsync();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return StatusCode(HttpStatusCode.InternalServerError);
            }
        }


    }
}
