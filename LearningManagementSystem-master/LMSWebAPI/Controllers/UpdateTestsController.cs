using LMSWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMSWebAPI.Controllers
{
    public class UpdateTestsController : ApiController
    {

        [Route("api/UpdateTests/{username}")]
        [HttpGet]
        public IHttpActionResult GetCourses([FromUri] string username)
        {
            LMSEntities1 db = new LMSEntities1();
            var result = db.usp_MyTests(username).ToList();
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [Route("api/UpdateTests/{username}/{courseid}")]
        [HttpPut]
        public IHttpActionResult PutTestScores([FromUri] string username, [FromUri] int courseid, [FromBody] Test test)
        {
            LMSEntities1 db = new LMSEntities1();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (username != test.UserName)
            {
                return BadRequest();
            }
            var std = db.Tests.Where<Test>(a => a.UserName == username);
            if (std != null)
            {
                foreach (Test t in std)
                {
                    if (t.CourseID == courseid)
                    {
                        t.UserName = test.UserName;
                        t.CourseID = test.CourseID;
                        t.TestDate = test.TestDate;
                        t.TestPercentage = test.TestPercentage;
                        t.TestRemarks = test.TestRemarks;
                        t.Certificate = test.Certificate;
                    }
                }
                var res = db.SaveChanges();
                if (res == 0)
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        [Route("api/UpdateTests/{username}")]
        [HttpPost]
        public IHttpActionResult PostCertificate([FromUri] string username, [FromBody] CertifiedUser cu)
        {
            LMSEntities1 db = new LMSEntities1();
            db.CertifiedUsers.Add(cu);
            db.SaveChanges();
            return Ok();
        }
    }
}
