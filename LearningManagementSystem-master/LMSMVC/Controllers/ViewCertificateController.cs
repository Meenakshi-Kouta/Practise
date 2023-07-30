using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using LMSMVC.Models;
using Microsoft.AspNet.Identity;

namespace LMSMVC.Controllers
{
    public class ViewCertificateController : Controller
    {
        // GET: ViewCertificate
        IEnumerable<ViewCertificate> certificate = null;
        public ActionResult Index()
        {
            var username = User.Identity.GetUserName();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/ViewCertificate/");
                var response = client.GetAsync("ViewCertificate?username=" + username.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<ViewCertificate>>();
                    readResponse.Wait();
                    certificate = readResponse.Result;

                }
                else
                {
                    certificate = Enumerable.Empty<ViewCertificate>();
                    ModelState.AddModelError(string.Empty, "Error occured. \t Contact Admin");
                }
            }

                return View(certificate);
            
        }

        public ActionResult Select()
        {
            return View("Select");
        }
    }
}