using Paradigmi.Progetto.Application.Requests;
using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Abstractions.Services
{
    public interface ILibroService
    {
        Task AddLibroAsync(Libro libro);
        Task<Libro> GetLibroAsync(string nome, string autore);
        Task<Libro> ModifyLibro(Libro libro, ModifyLibroRequest request);
    }
}
