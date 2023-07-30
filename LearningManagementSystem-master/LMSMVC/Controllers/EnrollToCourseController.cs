using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using LMSMVC.Models;


namespace LMSMVC.Controllers
{
    public class EnrollToCourseController : Controller
    {
        
        // GET: View Courses
        public ActionResult ViewCourse()
        {
            IEnumerable<Courses> course= null;
            using (var Client = new HttpClient())
            {
                Client.BaseAddress = new Uri("https://localhost:44334/api/Course");
                var response = Client.GetAsync("Course");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<Courses>>();
                    readResponse.Wait();

                    course = readResponse.Result;
                }
                else
                {
                    course = Enumerable.Empty<Courses>();
                    ModelState.AddModelError(string.Empty, "error occurerd contact admin");
                }
            }
            return View(course);
        }
        //to enroll to a course
        public ActionResult Create()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Create(Enroll e)
        {
            HttpClient hc = new HttpClient();
                hc.BaseAddress = new Uri("https://localhost:44334/api/");
            var insertRecord = hc.PostAsJsonAsync<Enroll>("EnrollToCourse", e);
            insertRecord.Wait();

            ViewBag.message = "Successfully Enrolled to a course";
                return View();
        }
    }
}