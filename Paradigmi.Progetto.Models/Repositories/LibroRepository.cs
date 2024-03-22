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
        public int GetLibroByNomeEAutore(string nome, string autore)
        {
            var query = _ctx.Libri.AsQueryable();
            query = query.Where((w => w.Nome.ToLower().Equals(nome) && w.Autore.ToLower().Equals(autore)));
            int result = query.Select(w => w.IdLibro).FirstOrDefault();
            return result == 0 ? -1 : result;
        }
        public async Task<Libro>? GetLibroAsyncById(int id)
        {
            var libro = await _ctx.Libri
                .Include( b=> b.Categorie)
                .ThenInclude( b=> b.Categoria)
                .FirstOrDefaultAsync(b => b.IdLibro == id);
            return libro;
        }
        public async Task ModifyLibro(Libro libro)
        {
            var entry = _ctx.Entry(libro);
            entry.Property(p => p.Autore).IsModified = true;
            entry.Property(p => p.Editore).IsModified = true;
            entry.Property(p => p.DataPubblicazione).IsModified = true;
            entry.Collection(p => p.Categorie).IsModified = true;
            await _ctx.SaveChangesAsync();
        }
        public Libro GetLibroById(int id)
        {
            var libro = _ctx.Libri
                .Include(b => b.Categorie)
                .ThenInclude(b => b.Categoria)
                .FirstOrDefault(b => b.IdLibro == id);
            return libro;
        }
    }
}
