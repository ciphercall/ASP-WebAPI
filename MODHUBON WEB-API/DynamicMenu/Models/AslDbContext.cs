using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using DynamicMenu.Models.Table;

namespace DynamicMenu.Models
{
    public class AslDbContext : DbContext
    {
        public DbSet<StkItemmst> StkItemmstsDbSet { get; set; }
        public DbSet<StkItem> StkItemsDbSet { get; set; }
        //public DbSet<AslUserco> AslUsercoDbSet { get; set; }
        //public DbSet<ASL_LOG> AslLogDbSet { get; set; }


        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        //}
    }
}