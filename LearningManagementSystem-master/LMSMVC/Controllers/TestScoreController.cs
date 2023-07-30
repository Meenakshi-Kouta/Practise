using LMSMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using LMSWebAPI.Models;

namespace LMSMVC.Controllers
{
    public class TestScoreController : Controller
    {
        // GET: TestScore
        public ActionResult Create()
        {
            if (ModelState.IsValid)
            {
                LMSEntities1 db = new LMSEntities1();
                db.Tests.Add(new Test
                {
                    UserName = User.Identity.GetUserName(),
                    TestID = 1,
                    TestDate = System.DateTime.Now,
                    TestPercentage = 75,
                    TestRemarks = "Pass",
                    Certificate = true
                });
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}