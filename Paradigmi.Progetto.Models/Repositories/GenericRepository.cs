using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Repositories
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected MyDbContext _ctx;
        public GenericRepository(MyDbContext ctx) {
            _ctx= ctx;
        }
        public async Task AggiungiAsync(T entity)
        {
           await _ctx.Set<T>().AddAsync(entity);
            //_ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Added;
        }
        public void Modifica(T entity)
        {
            _ctx.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        /*
         * metodo non async per i validators visto che risultano problematici con gli async
         */
        public T? Ottieni (int? id)
        {
            var response = _ctx.Set<T>().Find(id);
            return response;
        }
        public async Task<T> OttieniAsync(int id)
        {
           var response= await _ctx.Set<T>()
                .FindAsync(id);
            return response;

        }
        public void Elimina(T id)
        {
            _ctx.Entry(id).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
            /*_ctx.Set<T>()
                .Remove(id);*/
        }
        public async Task SaveAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
