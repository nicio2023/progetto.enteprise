using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Abstractions.Services
{
    public interface ILibroService
    {
        Task AddLibroAsync(Libro libro);
    }
}
