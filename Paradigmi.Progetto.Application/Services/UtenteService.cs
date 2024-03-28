using Paradigmi.Progetto.Application.Abstractions.Services;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Entities;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Services
{
    public class UtenteService : IUtenteService
    {
        private readonly IUtenteRepository _utenteRepository;
        public UtenteService (IUtenteRepository utenteRepository)
        {
            _utenteRepository = utenteRepository;
        }
        public async Task AddUtenteAsync(Utente utente)
        {
            await _utenteRepository.AggiungiAsync(utente);
            await _utenteRepository.SaveAsync();
        }
    }
}
