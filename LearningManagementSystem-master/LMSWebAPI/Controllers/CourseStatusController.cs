using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LMSWebAPI.Models;
using System.Data.SqlClient;

namespace LMSWebAPI.Controllers
{
    public class CourseStatusController : ApiController
    {
        [Route("api/CourseStatus/{name}")]
        [HttpGet, HttpPost]
        public IHttpActionResult GetCourseStatus([FromUri] string name)
        {
            LMSEntities1 sd = new LMSEntities1();
            var result = sd.usp_coursestatus(name).ToList();
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
