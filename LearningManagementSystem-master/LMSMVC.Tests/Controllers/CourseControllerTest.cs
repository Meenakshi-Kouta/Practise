using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMSMVC.Controllers;
using LMSMVC.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace LMSMVC.Tests.Controllers
{
    [TestClass]
    public class CourseControllerTest
    {
        [TestMethod]
        public void EditCourse()
        {
            //Arrange
            CourseController hc = new CourseController();
            Courses course = new Courses { CourseID = 105, CourseName = "Python", CourseDuration = "2h 18m", CourseApproval = true };
            //Act
            IEnumerable<Courses> course1 = hc.EditCourse(course);
            //Assert
            if (course1.Contains(course))
            {
                course.CourseName = "Python";
                IEnumerable<Courses> course2 = hc.EditCourse(course);
                Assert.AreEqual(course2.First(p => p.CourseID == course.CourseID).CourseName, "Python");
            }
        }
    }
}
