using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zeiterfassung_Domain
{
    public class Time
    {
        public string Activity { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public TimeSpan ResultTime { get; set; }

        [Display(Name = "Personalnummer")]
        public int EmployeeID { get; set; }

        [Display(Name = "Datum")]
        public DateTime Date
        {
            get
            {
                return DateTime.Today;
            }
            set
            {

            }
        }
    }
}