using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSMVC.Models
{
    public class UpdateStatus
    {
        public int SNo { get; set; }
        public string UserName{ get; set; }
        public short CourseID { get; set; }
        public decimal CourseStatus { get; set; }
        public string CourseCompletion { get; set; }
    }
}