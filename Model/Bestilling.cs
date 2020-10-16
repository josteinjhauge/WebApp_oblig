using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Oblig_2.Model
{
    public class Bestilling
    {
        public int bestillingId { get; set; }

        [Display(Name = "Dato")]
        [Required(ErrorMessage = "Dato må oppgis (dd/mm/yyyy)")]
        public string dato { get; set; }

        [Display(Name = "Tid")]
        [Required(ErrorMessage = "Tid må oppgis (24:00)")]
        public string tid { get; set; }

        [Display(Name = "Reise fra")]
        [Range(1, 100, ErrorMessage = "Stasjon må oppgis")]
        public int fraStasjon { get; set; }

        [Display(Name = "Reise til")]
        [Range(1, 100, ErrorMessage = "Stasjon må oppgis")]
        public int tilStasjon { get; set; }

        [Display(Name = "Pris")]
        public double pris { get; set; }
    }
}