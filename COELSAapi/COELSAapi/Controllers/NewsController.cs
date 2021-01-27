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
using COELSAapi.Models;
using COELSAapi.Models.DataModels;

namespace COELSAapi.Controllers
{
    public class NewsController : ApiController
    {
        private COELSADB_APIEntities db = new COELSADB_APIEntities();

        // GET: api/News
        public List<New> GetNews()
        {
            return db.News.ToList();
        }

        // GET: api/News/5
        [ResponseType(typeof(New))]
        public async Task<IHttpActionResult> GetNew(int id)
        {
            New @new = await db.News.FindAsync(id);
            if (@new == null)
            {
                return NotFound();
            }

            return Ok(@new);
        }

        // PUT: api/News/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutNew(int id, NewsData @new)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @newDB = db.News.FindAsync(id);

            if (id != @newDB.Id)
            {
                return BadRequest();
            }

            @new.Updated_At = DateTime.Now;

            db.Entry(@new).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewExists(id))
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

        // POST: api/News
        [ResponseType(typeof(New))]
        public async Task<IHttpActionResult> PostNew(NewsData newData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var @new = new New(newData.Title, newData.Link, newData.Context, DateTime.Now);

            db.News.Add(@new);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = @new.Id }, @new);
        }

        // DELETE: api/News/5
        [ResponseType(typeof(New))]
        public async Task<IHttpActionResult> DeleteNew(int id)
        {
            New @new = await db.News.FindAsync(id);
            if (@new == null)
            {
                return NotFound();
            }

            db.News.Remove(@new);
            await db.SaveChangesAsync();

            return Ok(@new);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewExists(int id)
        {
            return db.News.Count(e => e.Id == id) > 0;
        }
    }
}