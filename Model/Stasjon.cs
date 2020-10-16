using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Oblig_2.Model
{
    public class Stasjon
    {
        public int stasjonId { get; set; }

        [Display(Name = "Navn")]
        [Required(ErrorMessage = "Navn må oppgis")]
        public string navn { get; set; }

        [Display(Name = "Sone")]
        [Required(ErrorMessage = "Sone må oppgis")]
        public int sone { get; set; }
    }
}