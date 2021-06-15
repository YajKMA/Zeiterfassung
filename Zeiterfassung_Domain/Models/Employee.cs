using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Zeiterfassung_Domain
{
    public class Employee
    {
        [Display(Name = "Personalnummer")]
        public int EmployeeNumber { get; set; }

        [Required(ErrorMessage = "Ihr Vor- und Nachname wird benötigt")]
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Ihr Email-Adresse wird benötigt")]
        [Display(Name = "E-Mail")]
        public string Email { get; set; }

        [Required]
        [Display(Name = "E-Mail")]
        public string GivenEmail { get; set; }

        [Required]
        [Display(Name = "Passwort")]
        public string Password { get; set; }

        [Required]
        [Display(Name = "Passwort wiederholen")]
        public string RepeatPassword { get; set; }

        public bool Admin { get; set; }

        public bool FlagRemoved { get; set; }



    }
}
