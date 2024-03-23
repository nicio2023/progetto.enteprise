using FluentValidation;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class DeleteLibroRequestValidator : AbstractValidator<DeleteLibroRequest>
    {
        private readonly LibroRepository _libroRepository;
        public DeleteLibroRequestValidator(LibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
            RuleFor(x => x)
                .Must(entity => _libroRepository.GetLibroByNomeEAutore(entity.Nome.ToLower(), entity.Autore.ToLower()) != -1)
                .WithMessage("libro non esistente");
        }
        private void ValidaLibro(string value, ValidationContext<DeleteLibroRequest> context)
        {
            if (value == null)
            {
                context.AddFailure("il campo " + value + " non può essere nullo");
            }
            if (value.Length == 0)
            {
                context.AddFailure("il campo non può essere vuoto");
            }
        }

    }
}
