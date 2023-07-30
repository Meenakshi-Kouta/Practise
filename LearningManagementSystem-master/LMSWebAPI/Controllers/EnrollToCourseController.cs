using LMSWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMSWebAPI.Controllers
{
    public class EnrollToCourseController : ApiController
    {
        public IHttpActionResult EnrollUser(Enroll e)
        {
            LMSEntities1 ls = new LMSEntities1();
            ls.usp_EnrollUserToCourse(e.UserName, e.CourseID);
            ls.SaveChanges();
            return Ok();
        }
    }
}
