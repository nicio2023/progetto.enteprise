using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Entities
{
    public class CategoriaLibro
    {
        public int IdCategoriaLibro { get; set; }
        public Categoria? IdCategoria { get; set; }
        public Libro? IdLibro { get; set; }
    }
}
