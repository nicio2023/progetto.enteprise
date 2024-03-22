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
               .Custom(ValidaCategorieLibro);

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
        private void ValidaCategorieLibro(string value, ValidationContext<CreateLibroRequest> context)
        {
            if(_categoriaRepository.GetCategoriaByNome(value.ToLower()) == -1)
            {
                context.AddFailure("il campo " + value + " non esiste tra le categorie. Per accettare l'inserimento" +
                    " tutte le categorie inserite devono essere presenti.");
            }
        }
    }
}
