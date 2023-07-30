using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using LMSMVC.Models;

namespace LMSMVC.Controllers
{
    public class CourseController : Controller
    {
        #region TestContoller Methods

        public List<Courses> Read()
        {
            List<Courses> userList = new List<Courses>();
            return userList;
        }

        public List<Courses> CreateCourse(Courses newCourse)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44334/api/Course");

            //HTTP POST
            var postTask = client.PostAsJsonAsync<Courses>("Course", newCourse);
            postTask.Wait();

            var result = postTask.Result;

            List<Courses> userList = new List<Courses>();
            userList.Add(newCourse);
            return userList;
        }

        public List<Courses> EditCourse(Courses myCourse)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44334/api/Course");

            //HTTP POST
            var postTask = client.PostAsJsonAsync<Courses>("Course/", myCourse);
            postTask.Wait();

            var result = postTask.Result;

            List<Courses> userList = new List<Courses>();
            userList.Add(myCourse);
            return userList;
        }
        #endregion
        IEnumerable<Courses> courses = null;
        // GET: Course
        public ActionResult Index()
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                var response = client.GetAsync("Courses");
                response.Wait();
                var result = response.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<Courses>>();
                    readResponse.Wait();
                    courses = readResponse.Result;
                    //TempData["courseid"] = courses;
                }
                else
                {
                    courses = Enumerable.Empty<Courses>();
                    ModelState.AddModelError(string.Empty, "An Error Occured, Contact Admin");
                }
            }
            return View(courses);
        }

      
       

        // GET: Course/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Course/Create
        [HttpPost]
        public ActionResult Create(Courses course)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/Course");
                //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var post = client.PostAsJsonAsync<Courses>("Courses", course);
                post.Wait();
                var result = post.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    courses = Enumerable.Empty<Courses>();
                    ModelState.AddModelError(string.Empty, "An Error Occured \t Try Again Later");
                }
            }
            return View(course);
        }

        // GET: Course/Edit/5
        public ActionResult Edit(int id)
        {
            Courses course = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44334/api/");
                var response = client.GetAsync("Courses/" + id.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<Courses>();
                    readResponse.Wait();
                    course = readResponse.Result;
                }
                else
                {
                    //courses = Enumerable.Empty<Course>();
                    ModelState.AddModelError(string.Empty, "An Error Occured \t Try Again Later");
                }
            }
            return View(course);
        }

        // POST: Course/Edit/5
        [HttpPost]
        public ActionResult Edit(Courses course)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/Course");
                var put = client.PutAsJsonAsync<Courses>("Courses/" + course.CourseID.ToString(), course);
                put.Wait();
                var result = put.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    courses = Enumerable.Empty<Courses>();
                    ModelState.AddModelError(string.Empty, "An Error Occured. \t Try Again Later");
                }
            }
            return View(course);
        }

        // GET: Course/Delete/5
        public ActionResult Delete(int id)
        {
            Courses course = null;
            using (var client = new HttpClient())
            {

                client.BaseAddress = new Uri("https://localhost:44334/api/");
                var response = client.GetAsync("Courses/" + id.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<Courses>();
                    readResponse.Wait();
                    course = readResponse.Result;
                }
                else
                {
                    //courses = Enumerable.Empty<Course>();
                    ModelState.AddModelError(string.Empty, "An Error Occured. \t Try Again Later");
                }
            }
            return View(course);
        }

        // POST: Course/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Courses course)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/Course");
                var delete = client.DeleteAsync("Courses/" + id.ToString());
                delete.Wait();
                var result = delete.Result;

                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    courses = Enumerable.Empty<Courses>();
                    ModelState.AddModelError(string.Empty, "An Error Occured, Contact Admin");
                }
            }
            return RedirectToAction("Index");
        }
    }
}
