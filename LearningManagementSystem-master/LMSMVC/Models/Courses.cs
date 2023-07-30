using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSMVC.Models
{
    public class Courses
    {
        public short CourseID { get; set; }
        public string CourseName { get; set; }
        public string CourseDuration { get; set; }
        public bool CourseApproval { get; set; }
    }
}