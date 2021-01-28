using COELSAapi.Models;
using COELSAapi.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
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

        [HttpPost]
        [Route("Register")]
        public IHttpActionResult Register(UserData userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new User(userData.Name, userData.Email, userData.Password, userData.Role, DateTime.Now, userData.Company);

            db.Users.Add(user);
            db.SaveChanges();

            return CreatedAtRoute("api/Login", new { id = user.Id }, user);

        }
    }
}
