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
    public class CategoriaRepository : GenericRepository<Categoria>
    {
        public CategoriaRepository(MyDbContext ctx) : base(ctx)
        {

        }
        public int GetCategoriaByNome(string nome)
        {
            var query= _ctx.Categorie.AsQueryable();
            query = query.Where(w => w.Nome.ToLower().Equals(nome));
            int result = query.Select(w => w.IdCategoria).FirstOrDefault();
            return result == 0 ? -1 : result;
        }
        public async Task<bool> IscategoriaVuota(string name)
        {
            var categoria = await _ctx.Categorie
                .Include(c => c.Libri)
                .FirstOrDefaultAsync(c => c.Nome == name);
            return categoria?.Libri.Count == 0;
        }
        public async Task<Categoria>? GetCategoriaAsyncById(int id)
        {
            var categoria = await _ctx.Categorie.FindAsync(id);
            return categoria;
        }
    }
}
