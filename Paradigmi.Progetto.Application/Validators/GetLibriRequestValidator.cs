using FluentValidation;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class GetLibriRequestValidator : AbstractValidator<GetLibriRequest>
    {
        private readonly LibroRepository _libroRepository;
        public GetLibriRequestValidator(LibroRepository libroRepository) 
        {
            _libroRepository = libroRepository;
            RuleFor(x => x.Nome)
                .Custom(ValidaLibro);
            RuleFor(x => x.PageSize)
                .Must(x => x > 0)
                .WithMessage("inserire almeno un risultato per pagina");
            RuleFor(x => x.DataPubblicazione)
                .Must(x => x <= DateTime.Now || x==null)
                .WithMessage("La data di inserimento non può essere oltre quella attuale o deve essere nulla");
        }

        private void ValidaLibro(string value, ValidationContext<GetLibriRequest> context)
        {
            if (value == null)
            {
                context.AddFailure("il campo " + value + " non può essere nullo");
            }
            if (value.Length < 3)
            {
                context.AddFailure("il campo " + value + " deve avere almeno 3 caratteri");
            }
        }
    }
}
