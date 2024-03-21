using FluentValidation;
using Paradigmi.Progetto.Application.Requests;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class DeleteCategoriaRequestValidator : AbstractValidator<DeleteCategoriaRequest>
    {
        private readonly CategoriaRepository _categoriaRepository;

        public DeleteCategoriaRequestValidator(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;

            RuleFor(x => x.Nome)
                .Must(x => _categoriaRepository.GetCategoriaByNome(x.ToLower()) != -1)
                .WithMessage("categoria non esistente");
        }
    }
}
