using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Net.Http.Headers;
using LMSMVC.Models;
using System.Web.Http;

namespace LMSMVC.Controllers
{
    public class ApproveController : Controller
    {
        // GET: Approve
        public ActionResult Index()
        {
            IEnumerable<Approval> a = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                var response = client.GetAsync("EnrollmentRequests");
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<IList<Approval>>();
                    readResponse.Wait();
                    a = readResponse.Result;

                }
                else
                {

                    ModelState.AddModelError(string.Empty, "Error occured. \t Contact Admin");
                }
            }
            return View(a);
        }


        // GET: Approve/Edit/5
       
        public ActionResult Edit(int id)
        {
            Approval user = null;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                var response = client.GetAsync("EnrollmentRequests/" + id.ToString());
                response.Wait();
                var result = response.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readResponse = result.Content.ReadAsAsync<Approval>();
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

        // POST: Approve/Edit/5
        
        public ActionResult Edit(int id, Approval a)
        {
            try
            {
                // TODO: Add update logic here
                using (var client = new HttpClient())
                {
                    a.SNo = id;
                    client.BaseAddress = new Uri("https://localhost:44334/api/");
                    var put = client.PutAsJsonAsync<Approval>("EnrollmentRequests/" + a.SNo, a);
                    put.Wait();
                    var result = put.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Error occured.");
                    }
                }
                return View(a);
            }
            catch
            {
                return View();
            }
        }

        // GET: Approve/Delete/5
        
        public ActionResult Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44334/api/");
                var delete = client.DeleteAsync("EnrollmentRequests/" + id);
                delete.Wait();
                var result = delete.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return RedirectToAction("Index");
        }

    }
}
