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
using MyGamerSource.Models;

namespace MyGamerSource.Controllers
{
    public class useraccountsController : ApiController
    {
        private homedevEntities db = new homedevEntities();

        // GET: mygamer/useraccounts
        public IQueryable<useraccount> Getuseraccount()
        {
            return db.useraccount;
        }

        // GET: mygamer/useraccounts/5
        [ResponseType(typeof(useraccount))]
        public IHttpActionResult Getuseraccount(string id)
        {
            useraccount useraccount = db.useraccount.Find(id);
            if (useraccount == null)
            {
                return NotFound();
            }

            return Ok(useraccount);
        }

        // PUT: mygamer/useraccounts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putuseraccount(string id, useraccount useraccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != useraccount.email)
            {
                return BadRequest();
            }

            db.Entry(useraccount).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!useraccountExists(id))
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

        // POST: mygamer/useraccounts
        [ResponseType(typeof(useraccount))]
        public IHttpActionResult Postuseraccount(useraccount useraccount)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.useraccount.Add(useraccount);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (useraccountExists(useraccount.email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = useraccount.email }, useraccount);
        }

        // DELETE: mygamer/useraccounts/5
        [ResponseType(typeof(useraccount))]
        public IHttpActionResult Deleteuseraccount(string id)
        {
            useraccount useraccount = db.useraccount.Find(id);
            if (useraccount == null)
            {
                return NotFound();
            }

            db.useraccount.Remove(useraccount);
            db.SaveChanges();

            return Ok(useraccount);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool useraccountExists(string id)
        {
            return db.useraccount.Count(e => e.email == id) > 0;
        }
    }
}