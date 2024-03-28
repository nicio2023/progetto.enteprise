using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Abstractions.Services
{
    public interface IUtenteService
    {
        Task AddUtenteAsync(Utente utente);
    }
}
