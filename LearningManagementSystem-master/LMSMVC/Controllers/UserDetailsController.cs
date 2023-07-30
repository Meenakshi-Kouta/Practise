using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using LMSMVC.Models;
using LMSMVC;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace LMSMVC.Controllers
{
    public class UserDetailsController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        

        ApplicationDbContext context;
        public UserDetailsController()
        {
            context = new ApplicationDbContext();
        }

        public UserDetailsController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        #region TestContoller Methods
        public List<User> Read()
        {
            List<User> userList = new List<User>();
            return userList;
        }

        public List<User> CreateUser(User newUser)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44334/api/");

            //HTTP POST
            var postTask = client.PostAsJsonAsync<User>("Users", newUser);
            postTask.Wait();

            var result = postTask.Result;

            List<User> userList = new List<User>();
            userList.Add(newUser);
            return userList;
        }

        public List<User> EditUser(User myUser)
        {
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("https://localhost:44334/api/");

            //HTTP POST
            var postTask = client.PostAsJsonAsync<User>("Users", myUser);
            postTask.Wait();

            var result = postTask.Result;

            List<User> userList = new List<User>();
            userList.Add(myUser);
            return userList;
        }
        #endregion




        IEnumerable<User> user = null;
        // GET: UserDetails
        public ActionResult Index()
        {

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
  
                var response = client.GetAsync("Users");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<User>>();
                    readResponse.Wait();
                   
                    user = readResponse.Result;
                    //Session["user"] = user;
                }
                else
                {
                    user = Enumerable.Empty<User>();
                    ModelState.AddModelError(string.Empty, "Error occured. \t Contact Admin");
                }
            }
            return View(user);
        }

        // GET: UserDetails/Create
        public ActionResult Create()
        {
            ViewBag.Name = new SelectList(context.Roles.Where(m => !m.Name.Contains("Admin"))
                                    .ToList(), "Name", "Name");
            return View();
        }

        // POST: UserDetails/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(User u)
        {
            try
            {
                // TODO: Add insert logic here
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:44334/api/Users");
                    var post = client.PostAsJsonAsync<User>("Users", u);
                    post.Wait();
                    var result = post.Result;
                    var user = new ApplicationUser { UserName = u.UserName, Email = u.Email };
                    var res = await UserManager.CreateAsync(user, u.Password);
                    if (result.IsSuccessStatusCode)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        await this.UserManager.AddToRoleAsync(user.Id, u.UserRoles);
                        return RedirectToAction("Index");
                    }
                    ViewBag.Name = new SelectList(context.Roles.Where(m => !m.Name.Contains("Admin"))
                                  .ToList(), "Name", "Name");
                    
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(u);
            }
        }

        // GET: UserDetails/Edit/5
        public ActionResult Edit(string mail)
        {
            User user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/Users/");
                var response = client.GetAsync("User?Email=" + mail.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<User>();
                    readResponse.Wait();
                    user = readResponse.Result;
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error occured. \t Contact Admin");
                }
            }
            return View(user);
        }

        // POST: UserDetails/Edit/5
        [HttpPost]
        public ActionResult Edit(string mail, User u)
        {
            try
            {
                // TODO: Add update logic here
                using (var client = new HttpClient())
                {
                    u.Email = mail.ToString();
                    client.BaseAddress = new Uri("https://localhost:44334/api/Users/");
                    var put = client.PutAsJsonAsync<User>("User?Email=" + u.Email, u);
                    put.Wait();
                    var result = put.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error occured. \t Contact Admin");
                    }
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View(u);
            }
        }

        // GET: UserDetails/Delete/5
        public ActionResult Delete(string mail)
        {
            User user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/Users/");
                var response = client.GetAsync("User?Email=" + mail.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<User>();
                    readResponse.Wait();
                    user = readResponse.Result;
                }
                else
                {
                    //user = Enumerable.Empty<User>();
                    ModelState.AddModelError(string.Empty, "Error occured. \t Contact Admin");
                }
            }
            return View(user);
        }

        // POST: UserDetails/Delete/5
        [HttpPost]
        public ActionResult Delete(string mail, User u)
        {
            // TODO: Add delete logic here
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/Users/");
                var delete = client.DeleteAsync("User?mail=" + mail);
                delete.Wait();
                var result = delete.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "An Error Occured, Contact Admin");
                }
            }
            return RedirectToAction("Index");

        }
        public ActionResult CourseStatus(string name)
        {
            IEnumerable<Status> status = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/CourseStatus/");
                var response = client.GetAsync("CourseStatus?name=" + name.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<Status>>();
                    readResponse.Wait();
                    status = readResponse.Result;
                }
                else
                {
                    status = Enumerable.Empty<Status>();
                    ModelState.AddModelError(string.Empty, "Error occured. \t Contact Admin");
                }
            }
            return View(status);
        }

        public ActionResult CourseCompletion(string name)
        {
            IEnumerable<Completion> status = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/CourseCompletion/");
                var response = client.GetAsync("CourseCompletion?name=" + name.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<Completion>>();
                    readResponse.Wait();
                    status = readResponse.Result;
                }
                else
                {
                    status = Enumerable.Empty<Completion>();
                    ModelState.AddModelError(string.Empty, "Error occured. \t Contact Admin");
                }
            }
            return View(status);
        }
    }
}
