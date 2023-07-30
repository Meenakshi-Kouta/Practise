using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSMVC.Models
{
    public class LearnerProfile
    {
        public string Email { get; set; }
        public string UserName { get; set; }
        public short CourseID { get; set; }
        public string CourseName { get; set; }
        public System.DateTime EnrollmentDate { get; set; }
        public string CourseCompletion { get; set; }
    }
}