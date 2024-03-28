using Paradigmi.Progetto.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Abstractions
{
    public interface IUtenteRepository : IGenericRepository<Utente>
    {
        public int GetUtenteIndiceByPassword(string? password);
        public int GetUtenteIndiceByEmail(string? email);
        public Utente? GetUtenteByNomeCognomePassword(string? nome, string? cognome, string? password);
        public Task<Utente> GetUtenteByNomeCognomePasswordAsync(string nome, string cognome, string password);
    }
}
