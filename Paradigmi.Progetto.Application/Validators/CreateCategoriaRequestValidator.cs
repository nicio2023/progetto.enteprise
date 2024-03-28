using FluentValidation;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Application.Services;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class CreateCategoriaRequestValidator : AbstractValidator<CreateCategoriaRequest>
    {
        private readonly ICategoriaRepository _categoriaRepository;
        public CreateCategoriaRequestValidator (ICategoriaRepository categoriaRepository) {
            _categoriaRepository = categoriaRepository;

            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("il campo nome non può essere nullo")
                .MinimumLength(3)
                .WithMessage("il campo nome deve essere almeno lungo 3 caratteri")
                .Must(x => _categoriaRepository.GetCategoriaIndiceByNome(Spaces.RemoveExtraSpaces(x?.ToLower())) == -1)
                .WithMessage("categoria già presente");

        }

    }
}
