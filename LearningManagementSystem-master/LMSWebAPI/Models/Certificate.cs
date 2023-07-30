using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSWebAPI.Models
{
    public class Certificate
    {
        public string UserName { get; set; }
        public short CourseID { get; set; }
        public string CourseName { get; set; }
        public float TestPercentage { get; set; }
    }
}