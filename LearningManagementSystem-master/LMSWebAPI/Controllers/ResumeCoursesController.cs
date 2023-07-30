using System;
using System.Collections.Generic;
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
    public class ResumeCoursesController : ApiController
    {
        [Route("api/ResumeCourses/{username}")]
       
        public IHttpActionResult GetResumeCourse([FromUri] string username)
        {
            LMSEntities1 sd = new LMSEntities1();
            var result = sd.usp_name(username).ToList();
            if (result == null)
                return NotFound();
            return Ok(result);
        }

        [Route("api/ResumeCourses/{username}/{courseid}")]
        [HttpPut]
        public IHttpActionResult PutResumeCourse([FromUri] string username, [FromUri] Int16 courseid, [FromBody] Enrollment enrollment)
        {
            LMSEntities1 sd = new LMSEntities1();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (username != enrollment.UserName)
            {
                return BadRequest();
            }
            if (username == null)
            {
                return NotFound();
            }
            var std = sd.Enrollments.Where<Enrollment>(a => a.UserName == username);
            if (std != null)
            {
                foreach (Enrollment e in std)
                {
                    if (e.CourseID == courseid)
                    {
                        e.UserName = enrollment.UserName;
                        e.CourseID = enrollment.CourseID;
                        e.CourseStatus = enrollment.CourseStatus;
                        e.CourseCompletion = enrollment.CourseCompletion;
                    }
                }
                var res = sd.SaveChanges();
                if (res == 0)
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        [Route("api/ResumeCourses/{username}")]
        [HttpPost]
        public IHttpActionResult PostResumeCourse([FromUri] string username, [FromBody] Test test)
        {
            LMSEntities1 sd = new LMSEntities1();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            sd.Tests.Add(test);
            sd.SaveChanges();
            return Ok();
            
        }
    }
}
