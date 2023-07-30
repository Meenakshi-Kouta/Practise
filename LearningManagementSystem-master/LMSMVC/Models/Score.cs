using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMSMVC.Models
{
    public class Score
    {
        public short TestID { get; set; }
        public string UserID { get; set; }
        public short CourseID { get; set; }
        public Nullable<System.DateTime> TestDate { get; set; }
        public Nullable<decimal> TestPercentage { get; set; }
        public string TestRemarks { get; set; }
        public Nullable<bool> Certificate { get; set; }
    }
}