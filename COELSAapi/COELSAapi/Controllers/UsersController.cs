using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using COELSAapi.Models;
using COELSAapi.Models.Utils;
using static COELSAapi.Models.UtilCodes;
using AuthorizeAttribute = System.Web.Http.AuthorizeAttribute;

namespace COELSAapi.Controllers
{
    [Authorize]
    [AuthorizeEnum(Role.Admin)]
    public class UsersController : ApiController
    {
        private COELSADB_APIEntities db = new COELSADB_APIEntities();

        // GET: api/Users
        /// <summary>
        /// Devuelve una lista de usuarios
        /// </summary>        
        public List<User> GetUsers()
        {
            
            return db.Users.ToList();
        }

        // GET: api/Users/5
        /// <summary>
        /// Devuelve un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>        
        [ResponseType(typeof(User))]        
        public async Task<IHttpActionResult> GetUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        /// <summary>
        /// Modifica un usuario ya existente
        /// </summary>
        /// <param name="id">Id del usuario</param>
        /// <param name="user">User entity</param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, UserData user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var userDB = await db.Users.FindAsync(id);

            if (id != userDB.Id)
            {
                return BadRequest();
            }

            user.Updated_At = DateTime.Now;

            db.Entry(user).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Users
        /// <summary>
        /// Inserta un nuevo usuario en DB
        /// </summary>
        /// <param name="userData">User entity</param>        
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(UserData userData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            

            var user = new User(userData.Name, userData.Email, userData.Password, userData.Role, DateTime.Now, userData.Company);

            db.Users.Add(user);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        /// <summary>
        /// Borra un usuario
        /// </summary>
        /// <param name="id">Id del usuario</param>        
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await db.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            db.Users.Remove(user);
            await db.SaveChangesAsync();

            return Ok(user);
        }

        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        

        private bool UserExists(int id)
        {
            return db.Users.Count(e => e.Id == id) > 0;
        }
    }
}