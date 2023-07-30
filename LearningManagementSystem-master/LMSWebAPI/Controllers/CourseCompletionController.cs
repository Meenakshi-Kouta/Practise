using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using LMSWebAPI.Models;

namespace LMSWebAPI.Controllers
{
    public class CourseCompletionController : ApiController
    {
        [Route("api/CourseCompletion/{name}")]
        [HttpGet, HttpPost]
        public IHttpActionResult GetCourseCompletion(string name)
        {
            LMSEntities1 sd = new LMSEntities1();
            var result = sd.usp_coursecompletion(name).ToList();
            if (result == null)
                return NotFound();
            return Ok(result);
        }
    }
}
