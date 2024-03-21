using Paradigmi.Progetto.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Repositories
{
    public abstract class GenericRepository<T> where T : class
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
        public async Task<T> OttieniAsync(T id)
        {
           var response= await _ctx.Set<T>()
                .FindAsync(id);
            return response;

        }
        public async Task Elimina(T id)
        {
            _ctx.Entry(id).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }
        public async Task SaveAsync()
        {
            await _ctx.SaveChangesAsync();
        }
    }
}
