using FluentValidation;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class DeleteCategoriaRequestValidator : AbstractValidator<DeleteCategoriaRequest>
    {
        private readonly ICategoriaRepository _categoriaRepository;

        public DeleteCategoriaRequestValidator(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;

            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("il campo nome non può essere nullo")
                .NotEmpty()
                .WithMessage("il campo nome non può essere vuoto")
                .MinimumLength(3)
                .WithMessage("il campo nome deve avere almeno 3 caratteri");
            RuleFor(x => x.Nome)
                .Must(x => _categoriaRepository.GetCategoriaIndiceByNome(Spaces.RemoveExtraSpaces(x?.ToLower())) != -1)
                .WithMessage("categoria non esistente");
        }
    }
}
