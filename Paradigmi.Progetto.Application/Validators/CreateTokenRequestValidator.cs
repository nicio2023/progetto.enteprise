using FluentValidation;
using Paradigmi.Progetto.Application.Extensions;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class CreateTokenRequestValidator : AbstractValidator<CreateTokenRequest>
    {
        private readonly IUtenteRepository _utenteRepository;
        public CreateTokenRequestValidator(IUtenteRepository utenteRepository)
        {
            _utenteRepository = utenteRepository;

            RuleFor(x => x.Nome)
                .Custom(ValidaToken);
            
            RuleFor(x => x.Cognome)
                .Custom(ValidaToken);

            RuleFor(x => x.Password)
                .NotNull()
                .WithMessage("il campo password non può essere nullo")
                .MinimumLength(6)
                .WithMessage("il campo Password deve essere lungo almeno 6 caratteri")
                .RegEx("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+{}\\[\\]:;<>,.?~\\\\-]).{6,}$"
                , "Il campo password deve essere lungo almeno 6 caratteri e deve contenere almeno un carattere maiuscolo, uno minuscolo, un numero e un carattere speciale");

            RuleFor(x => x)
                .Must(x => _utenteRepository.GetUtenteByNomeCognomePassword(Spaces.RemoveExtraSpaces(x.Nome?.ToLower()), Spaces.RemoveExtraSpaces(x.Cognome?.ToLower()), x.Password) != null)
                .WithMessage("non esiste nessun utente con queste credenziali");

            }
        private void ValidaToken(string? value, ValidationContext<CreateTokenRequest> context)
        {
            if (value == null)
            {
                context.AddFailure("i campi nome e cognome non possono essere nulli");
            }
            else if (value.Length < 3)
            {
                context.AddFailure("i campi nome e cognome devono avere almeno 3 caratteri");
            }
        }
    }
}
