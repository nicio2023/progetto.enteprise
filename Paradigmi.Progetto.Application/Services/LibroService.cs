using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Models.Entities;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Services
{
    public class LibroService : ILibroService
    {
        private readonly LibroRepository _libroRepository;
        public LibroService (LibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }
        public async Task AddLibroAsync(Libro libro)
        {
            await _libroRepository.AggiungiAsync(libro);
            await _libroRepository.SaveAsync();
        }
    }
}
