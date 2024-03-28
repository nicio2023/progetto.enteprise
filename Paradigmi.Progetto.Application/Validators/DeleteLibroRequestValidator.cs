using FluentValidation;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class DeleteLibroRequestValidator : AbstractValidator<DeleteLibroRequest>
    {
        private readonly ILibroRepository _libroRepository;
        public DeleteLibroRequestValidator(ILibroRepository libroRepository)
        {
            _libroRepository = libroRepository;

            RuleFor(x => x.Nome)
                 .NotNull()
                 .WithMessage("il campo nome non può essere nulllo")
                 .MinimumLength(1)
                 .WithMessage("il campo libro deve avere almeno un carattere");

            RuleFor(x => x.Autore)
                .NotNull()
                .WithMessage("il campo autore non può essere nullo")
                .MinimumLength(3)
                .WithMessage("il campo autore deve avere almeno 3 caratteri");

            RuleFor(x => x)
                .Must(entity => _libroRepository.GetIndiceLibroByNomeEAutore(Spaces.RemoveExtraSpaces(entity.Nome?.ToLower()), Spaces.RemoveExtraSpaces(entity.Autore?.ToLower())) != -1)
                .WithMessage("libro non esistente");
        }

    }
}
