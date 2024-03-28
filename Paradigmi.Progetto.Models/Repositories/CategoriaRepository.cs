using Microsoft.EntityFrameworkCore;
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
    public class CategoriaRepository : GenericRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(MyDbContext ctx) : base(ctx)
        {

        }
        public int GetCategoriaIndiceByNome(string? nome)
        {
            int result = 0;
            if (nome != null)
            {
                var query = _ctx.Categorie.AsQueryable();
                query = query.Where(w => w.Nome.ToLower().Equals(nome));
                result = query.Select(w => w.IdCategoria).FirstOrDefault();
            }
            return result == 0 ? -1 : result;
        }
        public async Task<bool> IscategoriaVuotaAsync(string name)
        {
            var categoria = await _ctx.Categorie
                .Include(c => c.Libri)
                .FirstOrDefaultAsync(c => c.Nome.ToLower() == name.ToLower());
            return categoria?.Libri.Count == 0;
        }
    
        public async Task<List<Categoria>> GetCategorieByNomiAsync(List<string>? nomi)
        {
            var nomiMinuscoli = nomi.Select(s => s.ToLower()).ToList();
            var categorie = await _ctx.Categorie.Where(c =>(nomiMinuscoli.Contains(c.Nome.ToLower()))).ToListAsync();
            return categorie;
        }
        public async Task<Categoria> GetCategoriaByNomeAsync(string nome)
        {
            var categoria = await _ctx.Categorie.Where(c => nome.ToLower().Equals(c.Nome.ToLower())).FirstOrDefaultAsync();
            return categoria;
        }
        public void RemoveCategorieLibro(ICollection<CategoriaLibro> categorieLibro)
        {
             _ctx.RemoveRange(categorieLibro);
        }
    }
}
