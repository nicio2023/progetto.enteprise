using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Abstractions.Services
{
    public interface ILibroService
    {
        Task AddLibroAsync(Libro libro);
        Task<Libro> GetLibroAsync(string nome, string autore);
        Task<Libro> ModifyLibroAsync(Libro libro, ModifyLibroRequest request);
        Task DeleteLibroAsync(Libro libro);
        List<Libro> GetLibri(int from, int num, string nome, string autore, DateTime? data, out int totalNum);
    }
}
