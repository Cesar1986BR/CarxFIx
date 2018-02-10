using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Conserto.Models
{
    public class Db:DbContext
    {
        public Db():base("Conexao")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Conserto.Models.Usuario> Usuario { get; set; }
        public DbSet<Conserto.Models.Consertos> Conserto { get; set; }
        public DbSet<Conserto.Models.ConsertoDetalhes> ConsertoDetalhes{ get; set; }
        public DbSet<Conserto.Models.Pecas> Pecas { get; set; }
    }
}