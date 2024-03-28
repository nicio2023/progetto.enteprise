using Microsoft.AspNetCore.Mvc;
using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Entities;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Services
{
    public class CategoriaService : ICategoriaService
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CategoriaService(ICategoriaRepository categoriaRepository)
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

        public async Task<Categoria> GetCategoriaByNomeAsync(string name)
        {
            var categoria = await _categoriaRepository.GetCategoriaByNomeAsync(name);
            return categoria;
        }

        public Task<List<Categoria>> GetCategorieByNomiAsync(List<string> nomi)
        {
            return _categoriaRepository.GetCategorieByNomiAsync(nomi);
        }

        public Task<bool> IsCategoriaVuotaAsync(string name)
        {
            return _categoriaRepository.IscategoriaVuotaAsync(name);
        }
    }
}
