using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Abstractions.Services
{
    public interface ICategoriaService
    {
        Categoria GetCategoria(int id);
        Task AddCategoriaAsync(Categoria categoria);
        Task DeleteCategoriaAsync(Categoria categoria);
    }
}
