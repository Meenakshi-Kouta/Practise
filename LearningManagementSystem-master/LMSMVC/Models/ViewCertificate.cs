using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSMVC.Models
{
    public class ViewCertificate
    {

        public string UserName { get; set; }
        public short CourseID { get; set; }
        public string CourseName { get; set; }
        public float TestPercentage { get; set; }
    }
}