using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Oblig_2.Model
{
    public class Admin
    {
        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Navn må oppgis")]
        public string navn { get; set; }

        [Display(Name = "Passord")]
        [Required(ErrorMessage = "Passord må oppgis")]
        public string passord { get; set; }

    }
}
