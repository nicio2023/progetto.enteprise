using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Context;
using Paradigmi.Progetto.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Repositories
{
    /*
     * alcuni metodi non async perchè richiesti dai validators, i quali lavorano in modo sincrono
     */
    public class LibroRepository : GenericRepository<Libro>, ILibroRepository
    {
        public LibroRepository(MyDbContext ctx) : base(ctx)
        {

        }
        public int GetIndiceLibroByNomeEAutore(string? nome, string? autore)
        {
            int result = 0;
            if (nome != null && autore != null)
            {
                var query = _ctx.Libri.AsQueryable();
                query = query.Where((w => w.Nome.ToLower().Equals(nome) && w.Autore.ToLower().Equals(autore)));
                result = query.Select(w => w.IdLibro).FirstOrDefault();
            }
            return result == 0 ? -1 : result;
        }


        public async Task<Libro>? GetLibroByNomeEAutoreAsync(string nome, string autore)
        {
            var query = _ctx.Libri.AsQueryable();
            query = query.Where((w => w.Nome.ToLower().Equals(nome.ToLower()) && w.Autore.ToLower().Equals(autore.ToLower())));
            var result = await query.Select(w => w).FirstOrDefaultAsync();
            return result;
        }
        public List<Libro>? GetLibri(int from, int num, string? nome, string? autore, DateTime? data, string? Categoria, out int totalNum)
        {
            var query = _ctx.Libri.AsQueryable();
            query = query.Where(w => w.Nome.ToLower().Contains(nome.ToLower()));

            if (!string.IsNullOrEmpty(autore))
            {
                query = query.Where(w => w.Autore.ToLower().Contains(autore.ToLower()));
            }
            if (data != null)
            {
                query = query.Where(w => w.DataPubblicazione >= data);
            }
            if (!string.IsNullOrEmpty(Categoria))
            {
                query = query.Where(w => w.Categorie.Any(s => s.Categoria.Nome.ToLower().Contains(Categoria.ToLower())));
            }

            totalNum = query.Count();
            return
                query.
                OrderBy(d => d.DataPubblicazione)
                .Skip(from)
                .Take(num)
                .ToList();
        }
        public async Task<int> GetNumeroLibri(string? nome, string? autore)
        {
            var result = await _ctx.Libri.Where(w => w.Nome.ToLower().Equals(nome) && w.Autore.ToLower().Equals(autore)).ToListAsync();
            int total = result.Count();
            return total;
        }
    
    }
}
