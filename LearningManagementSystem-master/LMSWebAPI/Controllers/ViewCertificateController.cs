using LMSWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LMSWebAPI.Controllers
{
    public class ViewCertificateController : ApiController
    {
        [Route("api/ViewCertificate/{username}")]

        public IHttpActionResult GetCertificate([FromUri] string username)
        {
            LMSEntities1 sd = new LMSEntities1();
            var result = sd.usp_viewcertificate(username).ToList();
            if (result == null)
            return NotFound();
            return Ok(result);
        }
    }
}
