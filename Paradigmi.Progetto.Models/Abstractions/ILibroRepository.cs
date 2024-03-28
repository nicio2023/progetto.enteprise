using Paradigmi.Progetto.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Abstractions
{
    public interface ILibroRepository : IGenericRepository<Libro>
    {
        public int GetIndiceLibroByNomeEAutore(string? nome, string? autore);
        public Task<Libro>? GetLibroByNomeEAutoreAsync(string nome, string autore);
        public List<Libro>? GetLibri(int from, int num, string? nome, string? autore, DateTime? data, string? Categoria, out int totalNum);
        public Task<int> GetNumeroLibri(string? nome, string? autore);
    }
}
