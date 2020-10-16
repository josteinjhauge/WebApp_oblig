using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerEnabledDbContext;

namespace Oblig_2.DAL
{
    [TrackChanges]
    public class Adminen
    {
        [Key]
        public string Navn { get; set; }

        public byte[] Passord { get; set; }

        public byte[] Salt { get; set; }
    }

    public class AdminContext : TrackerContext
    {
        public AdminContext() : base("name=Admin")
        {
            Database.CreateIfNotExists();
        }

        public DbSet<Adminen> Adminen { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}

