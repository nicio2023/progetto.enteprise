using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Context;
using Paradigmi.Progetto.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Repositories
{
    public class UtenteRepository : GenericRepository<Utente>, IUtenteRepository
    {
        public UtenteRepository(MyDbContext _ctx) : base(_ctx)
        {

        }
        public int GetUtenteIndiceByPassword (string? password)
        {
            int result = 0;
            if(password != null)
            {
                var query = _ctx.Utenti.AsQueryable();
                query = query.Where(w => w.Password.Equals(password));
                result = query.Select(w => w.IdUtente).FirstOrDefault();
            }
            return result == 0 ? -1 : result;
        }
        public int GetUtenteIndiceByEmail(string? email)
        {
            int result = 0;
            if (email != null)
            {
                var query = _ctx.Utenti.AsQueryable();
                query = query.Where(w => w.Email.Equals(email));
                result = query.Select(w => w.IdUtente).FirstOrDefault();
            }
            return result == 0 ? -1 : result;
        }
        public Utente? GetUtenteByNomeCognomePassword(string? nome, string? cognome, string? password)
        {
            var utente = new Utente();

            if(nome!=null && cognome!=null && password != null)
            {
                var query = _ctx.Utenti.AsQueryable();
                utente = query.Where(w =>w.Nome.ToLower().Equals(nome) && w.Cognome.ToLower().Equals(cognome)).ToList().Where(u => u.Password==password).FirstOrDefault();
            }
            return utente;
        }
        public async Task<Utente> GetUtenteByNomeCognomePasswordAsync(string nome, string cognome, string password)
        {
            var utente = await _ctx.Utenti.Where(w => w.Nome.ToLower().Equals(nome) && w.Cognome.ToLower().Equals(cognome) && w.Password.Equals(password))
                .FirstOrDefaultAsync();
            return utente;
        }
    }
}
