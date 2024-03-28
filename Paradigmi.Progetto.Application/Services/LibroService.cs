using Microsoft.EntityFrameworkCore;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Context;
using Paradigmi.Progetto.Models.Entities;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Services
{
    public class LibroService : ILibroService
    {
        private readonly ILibroRepository _libroRepository;
        private readonly ICategoriaRepository _categoriaRepository;
        public LibroService (ILibroRepository libroRepository, ICategoriaRepository categoriaRepository)
        {
            _libroRepository = libroRepository;
            _categoriaRepository = categoriaRepository;
        }
        public async Task AddLibroAsync(Libro libro)
        {
            await _libroRepository.AggiungiAsync(libro);
            await _libroRepository.SaveAsync();
        }

        public async Task DeleteLibroAsync(Libro libro)
        {
            _libroRepository.Elimina(libro);
            await _libroRepository.SaveAsync();
        }

        public List<Libro> GetLibri(int from, int num, string nome, string? autore, DateTime? data,string? Categoria, out int totalNum)
        {
            return _libroRepository.GetLibri(from, num, nome, autore, data, Categoria, out totalNum);
        }

        public async Task<Libro>? GetLibroAsync(string nome, string autore)
        {
            var libro =await _libroRepository.GetLibroByNomeEAutoreAsync(nome, autore);
            return libro;
        }

        public async Task<Libro> ModifyLibroAsync(Libro libro, ModifyLibroRequest request)
        {
            _categoriaRepository.RemoveCategorieLibro(libro.Categorie);
            var categorie = await _categoriaRepository.GetCategorieByNomiAsync(request.CategorieModificate);
            libro.Categorie = categorie.Select(c => new CategoriaLibro { Categoria = c }).ToList();
            libro.Nome = request.NomeModificato;
            libro.Autore = request.AutoreModificato;
            libro.Editore = request.EditoreModificato;
            libro.DataPubblicazione = request.DataPubblicazioneModificata;
            _libroRepository.Modifica(libro);
            await _libroRepository.SaveAsync();
            return libro;
        }
        public async Task<int> GetNumeroLibri(string? nome, string? autore)
        {
            var total = await _libroRepository.GetNumeroLibri(nome, autore);
            return total;
        }
    }
}

