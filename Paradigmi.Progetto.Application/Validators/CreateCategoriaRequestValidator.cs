using FluentValidation;
using Paradigmi.Progetto.Application.Requests;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class CreateCategoriaRequestValidator : AbstractValidator<CreateCategoriaRequest>
    {
        private readonly CategoriaRepository _categoriaRepository;
        public CreateCategoriaRequestValidator (CategoriaRepository categoriaRepository) {
            _categoriaRepository = categoriaRepository;

            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("il campo nome non può essere nullo")
                .NotEmpty()
                .WithMessage("il campo nome non può essere vuoto")
                .MinimumLength(3)
                .WithMessage("il campo nome deve essere almeno lungo 3 caratteri")
                .Must(x => _categoriaRepository.GetCategoriaByNome(x.ToLower()) == -1)
                .WithMessage("categoria già presente");

        }

    }
}
