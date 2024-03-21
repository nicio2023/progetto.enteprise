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
        public Categoria GetCategoriaById(int id)
        {
            var query = _ctx.Categorie.AsQueryable();
            Categoria Categoria = (Categoria)_ctx.Categorie.Where(w => w.IdCategoria.Equals(id))
                .Select(w => w);
            return Categoria;
        }
    }
}
