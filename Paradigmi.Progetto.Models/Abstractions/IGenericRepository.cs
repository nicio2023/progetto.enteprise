using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Abstractions
{
    public interface IGenericRepository<T> where T : class
    {
        public Task AggiungiAsync(T entity);
        public void Modifica(T entity);
        public T? Ottieni(int? id);
        public Task<T> OttieniAsync(int id);
        public void Elimina(T id);
        public Task SaveAsync();
    }
}
