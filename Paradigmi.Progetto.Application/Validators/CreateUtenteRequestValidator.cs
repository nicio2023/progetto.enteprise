using FluentValidation;
using Paradigmi.Progetto.Application.Extensions;
using Paradigmi.Progetto.Application.Requests;

namespace Paradigmi.Progetto.Application.Validators
{
    public class CreateUtenteRequestValidator : AbstractValidator<CreateUtenteRequest>
    {
        public CreateUtenteRequestValidator() {
            RuleFor(x => x.Nome)
                .Custom(ValidaUtente);
            RuleFor(x => x.Cognome)
                .Custom(ValidaUtente);
            RuleFor(x => x.Password)
                .MinimumLength(6)
                .WithMessage("il campo Password deve essere lungo almeno 6 caratteri")
                .RegEx("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[!@#$%^&*()_+{}\\[\\]:;<>,.?~\\\\-]).{6,}$"
                , "Il campo password deve essere lungo almeno 6 caratteri e deve contenere almeno un carattere maiuscolo, uno minuscolo, un numero e un carattere speciale"
                );
            RuleFor(x => x.Email)
                .MinimumLength(6)
                .WithMessage("il campo Password deve essere lungo almeno 6 caratteri")
                .RegEx("[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+/.[a-zA-Z]{2,}$", "Il campo email deve essere lungo almeno 6 caratteri," +
                " prima del '@' devono esserci uno o più caratteri alfanumerici, puntini, trattini bassi, percentuali" +
                ", + e -, dopo il '@' e prima del '.' (quindi nel dominio dell'email), devono esserci uno o più caratteri" +
                " alfanumerici, punti e trattini; infine, dopo il '.', devono esserci due o più caratteri alfanumerici per il " +
                " dominio di primo livello");

        }
        private void ValidaUtente(string value, ValidationContext<CreateUtenteRequest> context)
        {
            if (value == null) {
                context.AddFailure("il campo "+value+" non può essere nullo");
            }
            if (value.Length < 3)
            {
                context.AddFailure("il campo " + value + " deve avere lunghezza >=3");
            }
        }
    }
}
