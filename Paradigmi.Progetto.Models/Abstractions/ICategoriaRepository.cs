using Paradigmi.Progetto.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Abstractions
{
    public interface ICategoriaRepository : IGenericRepository<Categoria>
    {
        public int GetCategoriaIndiceByNome(string? nome);
        public Task<bool> IscategoriaVuotaAsync(string name);
        public Task<List<Categoria>> GetCategorieByNomiAsync(List<string>? nomi);
        public Task<Categoria> GetCategoriaByNomeAsync(string nome);
        public void RemoveCategorieLibro(ICollection<CategoriaLibro> categorieLibro);

    }
}
