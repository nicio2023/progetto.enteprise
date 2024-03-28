using FluentValidation;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class CreateLibroRequestValidator : AbstractValidator<CreateLibroRequest>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        private readonly ILibroRepository _libroRepository;
        public CreateLibroRequestValidator(ICategoriaRepository categoriaRepository,ILibroRepository libroRepository) {

            _categoriaRepository = categoriaRepository;
            _libroRepository = libroRepository;

            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("il campo nome non può essere nulllo")
                .MinimumLength(1)
                .WithMessage("il campo libro deve avere almeno un carattere");

            RuleFor(x => x.Autore)
                .Custom(ValidaLibro);

            RuleFor( x => x.Editore)
                .Custom(ValidaLibro);

            RuleFor(x => x)
                .Must(x => _libroRepository.GetIndiceLibroByNomeEAutore(Spaces.RemoveExtraSpaces(x.Nome?.ToLower()), Spaces.RemoveExtraSpaces(x.Autore?.ToLower())) == -1)
                .WithMessage("libro già presente");

            RuleFor(x => x.DataPubblicazione)
               .NotNull()
               .WithMessage("il campo data pubblicazione non può essere nullo")
               .Must(x => x <= DateTime.Now)
               .WithMessage("La data di inserimento non può essere oltre quella attuale o deve essere nulla");

            RuleFor(x => x.Categorie)
               .Must(x => x != null)
               .WithMessage("la lista di categorie non può essere nulla");

            RuleForEach(x => x.Categorie)
                .Custom(ValidaLibro);

            RuleForEach(x => x.Categorie)
               .Custom(ValidaCategorieLibro);
            
        }
        private void ValidaLibro(string? value, ValidationContext<CreateLibroRequest> context)
        {
            if (value == null)
            {
                context.AddFailure("i campi autore e editore non possono essere nulli");
            }
            else if(value.Length < 3)
            {
                context.AddFailure("i campi autore e editore devono avere almeno 3 caratteri");
            }
        }
        private void ValidaCategorieLibro(string value, ValidationContext<CreateLibroRequest> context)
        {
            if(_categoriaRepository.GetCategoriaIndiceByNome(Spaces.RemoveExtraSpaces(value.ToLower())) == -1)
            {
                context.AddFailure("il campo <" + value + "> non esiste tra le categorie. Per accettare l'inserimento" +
                    " tutte le categorie inserite devono essere presenti.");
            }
        }
    }
}
