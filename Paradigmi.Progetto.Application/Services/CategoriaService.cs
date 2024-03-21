using Microsoft.AspNetCore.Mvc;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Models.Entities;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly CategoriaRepository _categoriaRepository;
        public CategoriaService(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public async Task AddCategoriaAsync(Categoria categoria)
        {
             await _categoriaRepository.AggiungiAsync(categoria);
             await _categoriaRepository.SaveAsync();
        }

        public async Task DeleteCategoriaAsync(Categoria categoria)
        {
            _categoriaRepository.Elimina(categoria);
            await _categoriaRepository.SaveAsync();
        }

        public Categoria GetCategoria(int id)
        {
            throw new NotImplementedException();
        }

    }
}
