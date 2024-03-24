using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        public List<Libro> GetLibri (int from, int num, string nome, string? autore, DateTime? data, string? Categoria, out int totalNum)
        {
            var query = _ctx.Libri.AsQueryable();
            int index=0;
            if (!string.IsNullOrEmpty(Categoria))
            {
                index = GetIndexCategoria(Categoria);
            }
            if(!string.IsNullOrEmpty(autore) || data != null || !string.IsNullOrEmpty(Categoria))
            {
                if(!string.IsNullOrEmpty(autore) && data != null && !string.IsNullOrEmpty(Categoria))
                {
                    query = GetLibriAutoreDataCategoria(nome, autore, data, index, query);
                }
                if (!string.IsNullOrEmpty(autore) && data!=null)
                {
                    query = GetLibriAutoreData(nome, autore, data, query);
                }
                if(!string.IsNullOrEmpty(autore) && !string.IsNullOrEmpty(Categoria))
                {
                    query = GetLibriAutoreCategoria(nome, autore, index, query);
                }
                else if (data !=null && !string.IsNullOrEmpty(Categoria)) 
                {
                    query = GetLibriDataCategoria(nome, data, index, query);
                }
            }
            else
            {
                query = query.Where(w => w.Nome.ToLower().Contains(nome.ToLower()));
            }
            totalNum = query.Count();
            return
                query.
                OrderBy(d => d.DataPubblicazione)
                .Skip(from)
                .Take(num)
                .ToList();
        }
        private int GetIndexCategoria(string Categoria)
        {
            int categoria = _ctx.Categorie
                .Where(c => c.Nome == Categoria)
                .Select(c => c.IdCategoria)
                .FirstOrDefault();
            //List<Libro> libri = new List<Libro>();
            /*if (categoria != 0)
            {
                    libri = _ctx.Libri
                    .Where(l => l.Categorie.Any(c => c.IdCategoria == categoria))
                    .ToList();
            }*/
            return categoria; 
        } 
        private IQueryable<Libro>? GetLibriAutoreDataCategoria(string nome,string autore, DateTime? data, int index, IQueryable<Libro>? query) {
        
           query = query.Where(w => w.Nome.ToLower().Contains(nome.ToLower()) && w.Autore.ToLower()
                    .Contains(autore.ToLower()) && w.DataPubblicazione >= data && w.Categorie.Any(c => c.IdCategoria == index));
            return query;

        }
        private IQueryable<Libro>? GetLibriAutoreData(string nome, string autore, DateTime? data, IQueryable<Libro>? query)
        {
            query = query.Where(w => w.Nome.ToLower().Contains(nome.ToLower()) && w.Autore.ToLower()
                    .Contains(autore.ToLower()) && w.DataPubblicazione >= data);
            return query;
        }
        private IQueryable<Libro>? GetLibriAutoreCategoria(string nome, string autore, int index, IQueryable<Libro>? query)
        {
            query = query.Where(w => w.Nome.ToLower().Contains(nome.ToLower()) && w.Autore.ToLower()
                    .Contains(autore.ToLower()) && w.Categorie.Any(c => c.IdCategoria == index));
            return query;
        }
        private IQueryable<Libro>? GetLibriDataCategoria(string nome, DateTime? data, int index, IQueryable<Libro>? query)
        {
            query = query.Where(w => w.Nome.ToLower().Contains(nome.ToLower()) &&
                        w.Categorie.Any(c => c.IdCategoria == index) && w.DataPubblicazione >= data);
            return query;
        }
    }
}
