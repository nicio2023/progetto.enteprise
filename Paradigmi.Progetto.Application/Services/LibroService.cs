using Microsoft.EntityFrameworkCore;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Requests;
using Paradigmi.Progetto.Models.Context;
using Paradigmi.Progetto.Models.Entities;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Services
{
    public class LibroService : ILibroService
    {
        private readonly LibroRepository _libroRepository;
        private readonly MyDbContext _ctx;
        public LibroService (LibroRepository libroRepository, MyDbContext ctx)
        {
            _libroRepository = libroRepository;
            _ctx = ctx;
        }
        public async Task AddLibroAsync(Libro libro)
        {
            await _libroRepository.AggiungiAsync(libro);
            await _libroRepository.SaveAsync();
        }
       public async Task<Libro> GetLibroAsync(string nome, string autore)
        {
            var index = _libroRepository.GetLibroByNomeEAutore(nome, autore);
            var libro = await _libroRepository.GetLibroAsyncById(index);
            return libro;
        }

        public async Task<Libro> ModifyLibro(Libro libro, ModifyLibroRequest request)
        {
            libro.Autore = request.AutoreModificato;
            libro.Editore = request.EditoreModificato;
            libro.DataPubblicazione = request.DataPubblicazioneModificata;
            var categorie = await _ctx.Categorie.Where(c => request.CategorieModificate.Contains(c.Nome)).ToListAsync();
            List<CategoriaLibro> categoriaLibri = categorie.Select(c => new CategoriaLibro { Categoria = c }).ToList();
            libro.Categorie= categoriaLibri;
            await _libroRepository.ModifyLibro(libro);
            return libro;
        }
    }
}
