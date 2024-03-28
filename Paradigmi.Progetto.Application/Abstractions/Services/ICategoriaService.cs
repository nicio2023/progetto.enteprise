using Paradigmi.Progetto.Models.Entities;
using System.Reflection.Metadata;

namespace Paradigmi.Progetto.Application.Abstractions.Services
{
    public interface ICategoriaService
    {
        Task AddCategoriaAsync(Categoria categoria);
        Task DeleteCategoriaAsync(Categoria categoria);
        Task<bool> IsCategoriaVuotaAsync(string name);
        Task<List<Categoria>> GetCategorieByNomiAsync(List<string> nomi);
        Task<Categoria> GetCategoriaByNomeAsync(string nome);
    }
}
