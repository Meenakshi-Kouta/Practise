using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSMVC.Models
{
    public class Status
    {
        public short CourseID { get; set; }
        public string CourseName { get; set; }
        public decimal CourseStatus { get; set; }
    }
}