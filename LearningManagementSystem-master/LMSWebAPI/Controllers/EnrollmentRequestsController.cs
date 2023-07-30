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
using LMSWebAPI.Models;

namespace LMSWebAPI.Controllers
{
    public class EnrollmentRequestsController : ApiController
    {
        private LMSEntities1 db = new LMSEntities1();

        // GET: api/EnrollmentRequests
        public IQueryable<EnrollmentRequest> GetEnrollmentRequests()
        {
            return db.EnrollmentRequests;
        }

        // GET: api/EnrollmentRequests/5
        [HttpGet, HttpPost]
        [Route("api/EnrollmentRequests/{id}")]
        [ResponseType(typeof(EnrollmentRequest))]
        public IHttpActionResult GetEnrollmentRequest([FromUri] int id)
        {
            EnrollmentRequest enrollmentRequest = db.EnrollmentRequests.Find(id);
            if (enrollmentRequest == null)
            {
                return NotFound();
            }

            return Ok(enrollmentRequest);
        }

        // PUT: api/EnrollmentRequests/5
        [HttpPut]
        [Route("api/EnrollmentRequests/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEnrollmentRequest(int id, EnrollmentRequest enrollmentRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != enrollmentRequest.SNo)
            {
                return BadRequest();
            }

            db.Entry(enrollmentRequest).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EnrollmentRequestExists(id))
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
        [HttpPost]
        // POST: api/EnrollmentRequests
        [ResponseType(typeof(EnrollmentRequest))]
        public IHttpActionResult PostEnrollmentRequest([FromUri] string username,[FromUri] short id)
        {
            LMSEntities1 db = new LMSEntities1();
            var course = db.Courses.First(d => d.CourseID == id);
            var user = db.AspNetUsers.First(d => d.UserName == username);
            if (course != null && user != null)
            {
                if (course.CourseApproval == true)
                {
                    EnrollmentRequest request = new EnrollmentRequest();
                    request.UserName = username;
                    request.CourseID = id;
                    db.EnrollmentRequests.Add(request);
                }
            }
            db.SaveChanges();

            return Ok();
        }

        // DELETE: api/EnrollmentRequests/5
        [HttpDelete]
        [ResponseType(typeof(EnrollmentRequest))]
        public IHttpActionResult DeleteEnrollmentRequest(int id)
        {
            EnrollmentRequest enrollmentRequest = db.EnrollmentRequests.Find(id);
            if (enrollmentRequest == null)
            {
                return NotFound();
            }

            db.EnrollmentRequests.Remove(enrollmentRequest);
            db.SaveChanges();

            return Ok(enrollmentRequest);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EnrollmentRequestExists(int id)
        {
            return db.EnrollmentRequests.Count(e => e.SNo == id) > 0;
        }
    }
}