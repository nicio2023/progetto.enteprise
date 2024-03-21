using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Requests
{
    public class CreateUtenteRequest
    {
        public string Nome { get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;   
        public string Password { get; set; } = string.Empty;
        public Utente ToEntity()
        {
            var request=new Utente();
            request.Nome = Nome;
            request.Cognome = Cognome;
            request.Email = Email;
            request.Password = Password;
            return request;
        }

    }
}
