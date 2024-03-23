using Microsoft.EntityFrameworkCore;
using Paradigmi.Progetto.Models.Context;
using Paradigmi.Progetto.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Repositories
{
    public class LibroRepository : GenericRepository<Libro>
    {
        public LibroRepository(MyDbContext ctx) : base(ctx)
        {

        }
        /*
         * metodo non async per i validators visto che risultano problematici con gli async
         */
        public int GetLibroByNomeEAutore(string nome, string autore)
        {
            var query = _ctx.Libri.AsQueryable();
            query = query.Where((w => w.Nome.ToLower().Equals(nome) && w.Autore.ToLower().Equals(autore)));
            int result = query.Select(w => w.IdLibro).FirstOrDefault();
            return result == 0 ? -1 : result;
        }

        public async Task ModifyLibroAsync(Libro libro)
        {
            var entry = _ctx.Entry(libro);
            entry.Property(p => p.Autore).IsModified = true;
            entry.Property(p => p.Editore).IsModified = true;
            entry.Property(p => p.DataPubblicazione).IsModified = true;
            entry.Collection(p => p.Categorie).IsModified = true;
            await _ctx.SaveChangesAsync();
        }

        public async Task<int> GetLibroByNomeEAutoreAsync(string nome, string autore)
        {
            var query = _ctx.Libri.AsQueryable();
            query = query.Where((w => w.Nome.ToLower().Equals(nome) && w.Autore.ToLower().Equals(autore)));
            int result = await query.Select(w => w.IdLibro).FirstOrDefaultAsync();
            return result == 0 ? -1 : result;
        }
        public List<Libro> GetLibri (int from, int num, string nome, string autore, DateTime? data, out int totalNum)
        {
            var query = _ctx.Libri.AsQueryable();
            if(!string.IsNullOrEmpty(autore) || data != null)
            {
                if(!string.IsNullOrEmpty(autore) && data != null)
                {
                    query = query.Where(w => w.Nome.ToLower().Contains(nome.ToLower()) && w.Autore.ToLower()
                    .Contains(autore.ToLower()) && w.DataPubblicazione >= data);
                }
                if (!string.IsNullOrEmpty(autore))
                {
                    query = query.Where(w => w.Nome.ToLower().Contains(nome.ToLower()) && w.Autore.ToLower()
                    .Contains(autore.ToLower()));
                }
                else
                {
                    query = query.Where(w => w.Nome.ToLower().Contains(nome.ToLower()) && w.DataPubblicazione
                    >= data);
                }
            }
            totalNum = query.Count();
            return
                query.
                OrderBy(d => d.DataPubblicazione)
                .Skip(from)
                .Take(num)
                .ToList();
        }
    }
}
