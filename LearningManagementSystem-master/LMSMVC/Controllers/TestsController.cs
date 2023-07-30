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
    public class TestsController : Controller
    {
        List<Tests> tests = new List<Tests>();
        // GET: Test

        public ActionResult Index()
        {
            IEnumerable<UpdateTests> test = null;
            var username = User.Identity.GetUserName();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/UpdateTests/");
                var response = client.GetAsync("UpdateTests?username=" + username.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<UpdateTests>>();
                    readResponse.Wait();
                    test = readResponse.Result;
                }
                else
                {
                    test = Enumerable.Empty<UpdateTests>();
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }
            return View(test);
        }

        public ActionResult CTest(short id)
        {
            ViewData["id"] = id;
            return View(tests);
        }
        public ActionResult CSharpTest(short id)
        {
            ViewData["id"] = id;
            return View(tests);
        }
        public ActionResult JavaTest(short id)
        {
            ViewData["id"] = id;
            return View(tests);
        }
        public ActionResult SQLTest(short id)
        {
            ViewData["id"] = id;
            return View(tests);
        }
        public ActionResult PythonTest(short id)
        {
            ViewData["id"] = id;
            return View(tests);
        }
        public ActionResult Submit()
        {
            return View(tests);
        }
        public ActionResult SubmitC()
        {
            //return Content("<script>alert('Error');</script>");
            return View(tests);
        }
        public ActionResult SubmitCSharp()
        {
            return View(tests);
        }
        public ActionResult SubmitJava()
        {
            return View(tests);
        }
        public ActionResult SubmitSQL()
        {
            return View(tests);
        }
        public ActionResult SubmitPython()
        {
            return View(tests);
        }

        // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Create(int id)
        {
            return View();
        }
        // GET: Test/Create
        [HttpPost]
        public ActionResult Create(Tests test)
        {
            return View();
        }
       
        // POST: Test/Edit/5
        [HttpPost]
        public ActionResult EditScore(short id, string score)
        {
            string username = User.Identity.GetUserName();
            Score s = new Score();
            Certified c = new Certified();
            c.UserID = username;
            c.CourseID = id;
            s.UserID = username;
            s.CourseID = id;
            s.TestDate = DateTime.Now;
            s.TestPercentage = Convert.ToDecimal(score);
            if (s.TestPercentage >= 60)
            {
                s.TestRemarks = "Pass";
                s.Certificate = true;
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44334/api/");
                    var post = client.PostAsJsonAsync<Certified>("UpdateTests?username=" + username, c);
                    post.Wait();
                    var result = post.Result;
                    if (result.IsSuccessStatusCode)
                    {

                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error Occured");
                    }
                }
            }
            else
            {
                s.TestRemarks = "Fail";
                s.Certificate = false;
            }

            //s.TestPercentage = score;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                var put = client.PutAsJsonAsync<Score>("UpdateTests/" + username + "/" + id, s);
                put.Wait();
                var result = put.Result;

                if (result.IsSuccessStatusCode)
                {

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error Occured");
                }
            }


            return RedirectToAction("Index");


        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Select(short id, string name)
        {
            if (name == "C")
                return RedirectToAction("CTest", new { id = id });
            else if (name == "C#")
                return RedirectToAction("CSharpTest", new { id = id });
            else if (name == "Java")
                return RedirectToAction("JavaTest", new { id = id });
            else if (name == "Sql")
                return RedirectToAction("SQLTest", new { id = id });
            else if (name == "Python")
                return RedirectToAction("PythonTest", new { id = id });
            return RedirectToAction("Index");
        }

    }
}