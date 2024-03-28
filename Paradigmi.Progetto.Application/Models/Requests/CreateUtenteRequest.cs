using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Models.Requests
{
    public class CreateUtenteRequest
    {
        public string? Nome { get; set; } = string.Empty;
        public string? Cognome { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public Utente ToEntity()
        {
            var request = new Utente();
            request.Nome = Spaces.RemoveExtraSpaces(Nome);
            request.Cognome = Spaces.RemoveExtraSpaces(Cognome);
            request.Email = Email;
            request.Password = Password;
            return request;
        }

    }
}
