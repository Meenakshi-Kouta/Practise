using System;
using System.Collections.Generic;
using System.Linq;
using LMSMVC.Controllers;
using System.Net.Http;
using LMSMVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LMSMVC.Tests.Controllers
{
    [TestClass]
    public class UserControllerTest
    {
        [TestMethod]
        public void EditUser()
        {
            //Arrange
            UserDetailsController gc = new UserDetailsController();
            User cus = new User { Email = "User1@mail.com", UserName = "User1", Password = "User1@123", ConfirmPassword = "User1@123" };

            //Act

            IEnumerable<User> custs = gc.EditUser(cus);

            //Assert
            if (custs.Contains(cus))
            {
                cus.UserName = "User1";
                IEnumerable<User> guests2 = gc.EditUser(cus);
                Assert.AreEqual(guests2.First(p => p.Email == cus.Email).UserName, "User1");
            }
        }
    }
}
