using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LMSWebAPI.Models;

namespace LMSWebAPI.Controllers
{
    public class UsersController : ApiController
    {
        private LMSEntities1 db = new LMSEntities1();

        // GET: api/Users
        public IQueryable<AspNetUser> GetUsers()
        {
            return db.AspNetUsers;
        }

        // GET: api/Users/5
        [ResponseType(typeof(AspNetUser))]
        [HttpGet, HttpPost]
        [Route("api/Users/{Email}")]
        public IHttpActionResult GetUser([FromUri] string Email)
        {
            AspNetUser user = db.AspNetUsers.First(u => u.Email == Email);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        // PUT: api/Users/5
        [ResponseType(typeof(void))]
        [HttpPut]
        [Route("api/Users/{Email}")]
        public IHttpActionResult PutUser([FromUri] string Email, AspNetUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (Email != user.Email)
            {
                return BadRequest();
            }

            var std = db.AspNetUsers.First(a => a.Email == Email);
         
            std.UserName = user.UserName;
            std.Email = user.Email;
            

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Email))
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
        [ResponseType(typeof(AspNetUser))]
        public IHttpActionResult PostUser(AspNetUser user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AspNetUsers.Add(user);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Email))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = user.Email }, user);
        }

        // DELETE: api/Users/5
        [ResponseType(typeof(AspNetUser))]
        [HttpDelete]
        [Route("api/Users/{Email}")]
        public IHttpActionResult DeleteUser(string email)
        {
            AspNetUser user = db.AspNetUsers.First(u => u.Email == email);
            if (user == null)
            {
                return NotFound();
            }

            db.AspNetUsers.Remove(user);
            db.SaveChanges();

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

        private bool UserExists(string id)
        {
            return db.AspNetUsers.Count(e => e.Email == id) > 0;
        }
    }
}
