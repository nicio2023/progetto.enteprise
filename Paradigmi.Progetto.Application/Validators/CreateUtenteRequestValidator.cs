using FluentValidation;
using Paradigmi.Progetto.Application.Extensions;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class CreateUtenteRequestValidator : AbstractValidator<CreateUtenteRequest>
    {
        private readonly IUtenteRepository _utenteRepository;
        public CreateUtenteRequestValidator(IUtenteRepository utenteRepository) {

            _utenteRepository = utenteRepository ;

            RuleFor(x => x.Nome)
                .Custom(ValidaUtente);
            RuleFor(x => x.Cognome)
                .Custom(ValidaUtente);
            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("il campo password non può essere nullo")
                .MinimumLength(6)
                .WithMessage("il campo Password deve essere lungo almeno 6 caratteri")
                .RegEx("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+{}\\[\\]:;<>,.?~\\\\-]).{6,}$"
                , "Il campo password deve essere lungo almeno 6 caratteri e deve contenere almeno un carattere maiuscolo, uno minuscolo, un numero e un carattere speciale");

            RuleFor(x => x)
                .Must(x => _utenteRepository.GetUtenteIndiceByPassword(x.Password) == -1)
                .WithMessage("Password già esistente");
            
            RuleFor(x => x.Email)
                .NotNull()
                .WithMessage("il campo email non può essere nullo")
                .RegEx("[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+.[a-zA-Z]{2,}$", " prima del '@' l'email può contenere lettere (sia maiuscole che minuscole), numeri e alcuni caratteri speciali come il punto (.), " +
                "l'underscore (_), il segno di percentuale (%), il segno più (+) e il segno meno (-). Dopo il '@' e prima del '.' (quindi nel dominio dell'email), che può contenere lettere (sia maiuscole che minuscole), " +
                "numeri e il carattere punto (.) e il segno meno (-).Infine, dopo il '.', devono esserci due o più lettere (non importa se maiuscole o minuscole) per il dominio di primo livello");

            RuleFor(x => x)
                .Must(x => _utenteRepository.GetUtenteIndiceByEmail(x.Email) == -1)
                .WithMessage("email già esistente");

        }
        private void ValidaUtente(string? value, ValidationContext<CreateUtenteRequest> context)
        {
            if (value == null) {
                context.AddFailure("i campi nome e cognome non possono essere nulli");
            }
            else if (value.Length < 3)
            {
                context.AddFailure("i campi nome e cognome devono avere lunghezza >=3");
            }
        }
    }
}
