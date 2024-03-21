using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Entities
{
    public class Categoria
    {
        public int IdCategoria { get; set; }
        public string  Nome { get; set; } = string.Empty;
        public ICollection<Libro> Libri { get; set; } = null!;

    }
}
