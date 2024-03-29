using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Models.Dtos
{
    public class UtenteDto
    {
        public int IdUtente { get;set ; }
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public UtenteDto(Utente utente)
        {
            IdUtente = utente.IdUtente;
            Nome = utente.Nome;
            Cognome = utente.Cognome;
            Email = utente.Email;
            Password = utente.Password;
        }
    }
}
