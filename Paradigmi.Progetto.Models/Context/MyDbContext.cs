using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Models.Context
{
    public class MyDbContext : DbContext
    {
        public DbSet<Utente> Utenti { get; set; }
        public DbSet<Libro> Libri { get; set; }
        public DbSet<Categoria> Categorie { get; set; }
        public MyDbContext(DbContextOptions<MyDbContext> config) : base(config)
        {

        }

        public MyDbContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
               //.UseLazyLoadingProxies()
               .UseSqlServer("data source=DESKTOP-82M9ESU;Initial catalog=Paradigmi;User Id=utente;Password=password;TrustServerCertificate=True");

            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoriaLibro>()
                .HasKey(lc => new { lc.IdLibro, lc.IdCategoria });

            modelBuilder.Entity<CategoriaLibro>()
                .HasOne(lc => lc.Libro)
                .WithMany(l => l.Categorie)
                .HasForeignKey(lc => lc.IdLibro);

            modelBuilder.Entity<CategoriaLibro>()
                .HasOne(lc => lc.Categoria)
                .WithMany(c => c.Libri)
                .HasForeignKey(lc => lc.IdCategoria);

            modelBuilder.ApplyConfigurationsFromAssembly(this.GetType().Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
