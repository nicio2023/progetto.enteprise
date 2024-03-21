using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Entities
{
    public class CategoriaLibro
    {
        public int IdCategoria { get; set; }
        public Categoria Categoria { get; set; }
        public int IdLibro { get; set; }
        public Libro Libro { get; set; }

    }
}
