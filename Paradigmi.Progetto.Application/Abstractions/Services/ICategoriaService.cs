using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Abstractions.Services
{
    public interface ICategoriaService
    {
        Task<Categoria> GetCategoriaAsync(string name);
        Task AddCategoriaAsync(Categoria categoria);
        Task DeleteCategoriaAsync(Categoria categoria);
        Task<bool> IsCategoriaVuota(string name);
    }
}
