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
    public class CategoriaLibroConfiguration : IEntityTypeConfiguration<CategoriaLibro>
    {
        public void Configure(EntityTypeBuilder<CategoriaLibro> builder)
        {
            builder.ToTable("CategoriaLibro");
            
            builder.HasKey(lc => lc.IdCategoriaLibro);
            
            builder.HasOne(lc => lc.Libro)
                .WithMany(l => l.Categorie)
                .HasForeignKey(lc => lc.IdLibro)
                .OnDelete(DeleteBehavior.Cascade);
            
            builder.HasOne(lc => lc.Categoria)
                .WithMany(c => c.Libri)
                .HasForeignKey(lc => lc.IdCategoria)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
