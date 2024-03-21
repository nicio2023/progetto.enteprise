using FluentValidation;
using Paradigmi.Progetto.Application.Requests;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class CreateLibroRequestValidator : AbstractValidator<CreateLibroRequest>
    {
        private readonly CategoriaRepository _categoriaRepository;
        public CreateLibroRequestValidator(CategoriaRepository categoriaRepository) {

            _categoriaRepository = categoriaRepository;

            RuleFor(x => x.Nome)
                .Custom(ValidaLibro);
            RuleFor(x => x.Autore)
                .Custom(ValidaLibro);
            RuleFor( x => x.Editore)
                .Custom(ValidaLibro);
            RuleForEach(x => x.Categorie)
               .NotNull()
               .WithMessage("il campo nome non può essere nullo")
               .NotEmpty()
               .WithMessage("il campo nome non può essere vuoto")
               .MinimumLength(3)
               .WithMessage("il campo nome deve essere almeno lungo 3 caratteri")
               .Must(x => _categoriaRepository.GetCategoriaByNome(x.ToLower()) != -1)
               .WithMessage("categoria non esistente");

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
