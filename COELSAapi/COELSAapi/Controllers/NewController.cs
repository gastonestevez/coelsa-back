using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using COELSAapi.Models;

namespace COELSAapi.Controllers
{
    public class NewController : ApiController
    {
        private PocoCOELSA_APIEntities db = new PocoCOELSA_APIEntities();

        // GET: api/New
        public IQueryable<New> GetNews()
        {
            return db.News;
        }

        // GET: api/New/5
        [ResponseType(typeof(New))]
        public IHttpActionResult GetNew(int id)
        {
            New @new = db.News.FirstOrDefault(n => n.Id == id);
            if (@new == null)
            {
                return NotFound();
            }

            return Ok(@new);
        }

        // PUT: api/New/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNew(int id, New @new)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != @new.Id)
            {
                return BadRequest();
            }

            //db.Entry(@new).State = EntityState.Modified;
            db.News.ApplyCurrentValues(@new);             
            db.SaveChanges();

            try
            {
                db.SaveChanges();
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

        // POST: api/New
        [ResponseType(typeof(New))]
        public IHttpActionResult PostNew(New @new)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.News.AddObject(@new);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = @new.Id }, @new);
        }

        // DELETE: api/New/5
        [ResponseType(typeof(New))]
        public IHttpActionResult DeleteNew(int id)
        {
            New @new = db.News.FirstOrDefault(n => n.Id == id);
            if (@new == null)
            {
                return NotFound();
            }

            db.News.DeleteObject(@new);
            db.SaveChanges();

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