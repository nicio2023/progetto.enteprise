using FluentValidation;
using Paradigmi.Progetto.Application.Requests;

namespace Paradigmi.Progetto.Application.Validators
{
    public class CreateLibroRequestValidator : AbstractValidator<CreateLibroRequest>
    {
        public CreateLibroRequestValidator() {
            RuleFor(x => x.Nome)
                .Custom(ValidaLibro);
            RuleFor(x => x.Autore)
                .Custom(ValidaLibro);
            RuleFor( x => x.Editore)
                .Custom(ValidaLibro);
        }
        private void ValidaLibro(string value, ValidationContext<CreateLibroRequest> context)
        {
            if (value == null)
            {
                context.AddFailure("il campo " + value + " non può essere nullo");
            }
            if(value.Length < 3)
            {
                context.AddFailure("il campo " + value + " deve avere almeno 3 caratteri");
            }
        }
    }
}
