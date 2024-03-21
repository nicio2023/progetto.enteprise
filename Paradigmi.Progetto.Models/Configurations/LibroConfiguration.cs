using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Paradigmi.Progetto.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Configurations
{
    public class LibroConfiguration : IEntityTypeConfiguration<Libro>
    {
        public void Configure(EntityTypeBuilder<Libro> builder)
        {
            builder.ToTable("Libro");
            builder.HasKey(p => p.IdLibro);
            builder.Property(p => p.Autore).HasMaxLength(50);
            builder.Property(p => p.Editore).HasMaxLength(50);
            builder.Property(p => p.Nome).HasMaxLength(50);
            /*builder.HasMany(l => l.Categorie)
                .WithMany(c => c.Libri)
                .UsingEntity<CategoriaLibro>(
                j => j.HasOne(lc => lc.IdCategoria).WithMany(),
                j => j.HasOne(lc => lc.IdLibro).WithMany())
                .HasKey(lc => new { lc.IdCategoriaLibro });*/

        }
    }
}
