using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSMVC.Models
{
    public class Approval
    {
        public int SNo { get; set; }
        public string Email { get; set; }
        public short CourseID { get; set; }
        public Nullable<bool> CourseApproval { get; set; }
    }
}