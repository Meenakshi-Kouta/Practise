using LMSMVC.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace LMSMVC.Controllers
{
    public class LearnerProfileController : Controller
    {
        IEnumerable<LearnerProfile> profile = null;

        // GET: LearnerProfile
        public ActionResult GetProfile()
        {
            var username = User.Identity.GetUserName();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/LearnerProfile/");
                var response = client.GetAsync("LearnerProfile?username=" + username.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<LearnerProfile>>();
                    readResponse.Wait();
                    profile = readResponse.Result;
                }
                else
                {
                   profile = Enumerable.Empty<LearnerProfile>();
                    ModelState.AddModelError(string.Empty, "Error occured. \t Contact Admin");
                }
            }
            return View(profile);
        }
    }
}