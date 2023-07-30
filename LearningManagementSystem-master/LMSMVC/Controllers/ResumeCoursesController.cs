using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using LMSMVC.Models;
using Microsoft.AspNet.Identity;

namespace LMSMVC.Controllers
{
    public class ResumeCoursesController : Controller
    {


        // GET: Course
        IEnumerable<ResumeCourse> course = null;

        public ActionResult Index()
        {
            var username = User.Identity.GetUserName();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/ResumeCourses/");
                var response = client.GetAsync("ResumeCourses?username=" + username.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<ResumeCourse>>();
                    readResponse.Wait();
                    course = readResponse.Result;
                    //TempData["course"] = course;
                }
                else
                {
                    course = Enumerable.Empty<ResumeCourse>();
                    ModelState.AddModelError(string.Empty, "Error occured. \t Contact Admin");
                }
            }


            return View(course);
        }


        public ActionResult Select(short id, string name)
        {
            if (name == "C")
                return RedirectToAction("Cprog", new { id = id });
            else if (name == "C#")
                return RedirectToAction("CSharpprog", new { id = id });
            else if (name == "Java")
                return RedirectToAction("Javaprog", new { id = id });
            else if (name == "Sql")
                return RedirectToAction("SQLprog", new { id = id });
            else if (name == "Python")
                return RedirectToAction("Pythonprog", new { id = id });
            return RedirectToAction("Index");
        }



        // GET: Cours/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cours/Create
        public ActionResult Create()
        {

            return View(course);
        }
        public ActionResult Cprog(short id)
        {
            ViewData["id"] = id;
            return View("Cprog");
        }
        public ActionResult CSharpprog(short id)
        {
            ViewData["id"] = id;
            return View("CSharpprog");
        }
        public ActionResult Javaprog(short id)
        {
            ViewData["id"] = id;
            return View("Javaprog");
        }
        public ActionResult SQLprog(short id)
        {
            ViewData["id"] = id;
            return View("SQLprog");
        }
        public ActionResult Pythonprog(short id)
        {
            ViewData["id"] = id;
            return View("Pythonprog");
        }
        // POST: Cours/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cours/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: Cours/Edit/5
        //[HttpPost]
        public ActionResult Edit(short id)
        {
            var username = User.Identity.GetUserName();
            UpdateStatus e = new UpdateStatus();
            Tests t = new Tests();
            e.UserName = username;
            e.CourseID = id;
            e.CourseStatus = 100;
            e.CourseCompletion = "Completed";
            t.UserName = e.UserName;
            t.CourseID = e.CourseID;

            using (var client = new HttpClient())
            {
                //cObj.CourseID = id;
                client.BaseAddress = new Uri("https://localhost:44334/api/ResumeCourses/");
                var put = client.PutAsJsonAsync<UpdateStatus>("ResumeCourses/" + username + "/" + id, e);
                put.Wait();
                var result = put.Result;
                var post = client.PostAsJsonAsync<Tests>("ResumeCourses/ResumeCourses?username=" + username, t);
                post.Wait();
                var res = post.Result;

                if (result.IsSuccessStatusCode && res.IsSuccessStatusCode)
                {
                    //return RedirectToAction("Index");
                }
                else
                {
                    //courses = Enumerable.Empty<UpdateStatus>();
                    ModelState.AddModelError(string.Empty, "An Error Occured, Contact Admin");
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Cours/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cours/Delete/5
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
    }
}
