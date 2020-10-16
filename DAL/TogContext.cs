using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TrackerEnabledDbContext;

namespace Oblig_2.DAL
{
    [TrackChanges]
    public class Bestillinger
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BestillingerId { get; set; }

        [Required]
        public string Dato { get; set; }

        [Required]
        public string Tid { get; set; }

        [Required]
        public double Pris { get; set; }

        public virtual Stasjoner FraStasjon { get; set; }

        public virtual Stasjoner TilStasjon { get; set; }
    }

    [TrackChanges]
    public class Stasjoner
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int StasjonerId { get; set; }

        [Required]
        public string Navn { get; set; }

        [Required]
        public int Sone { get; set; }
    }

    [TrackChanges]
    public class Priser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PrisId { get; set; }

        [Required]
        public double SonePris { get; set; }
    }

    public class TogContext : TrackerContext
    {
        public TogContext() : base("name=Tog")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Bestillinger> Bestillinger { get; set; }

        public DbSet<Stasjoner> Stasjoner { get; set; }

        public DbSet<Priser> Pris { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}