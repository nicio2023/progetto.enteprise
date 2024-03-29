using FluentValidation;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.Services;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class GetLibriRequestValidator : AbstractValidator<GetLibriRequest>
    {
        public GetLibriRequestValidator() 
        {
            RuleFor(x => x.Nome)
                .NotNull()
                .WithMessage("il campo nome non può essere nulllo")
                .MinimumLength(1)
                .WithMessage("il campo nome deve avere almeno un carattere");

            RuleFor(x => x.PageSize)
                .Must(x => x > 0)
                .WithMessage("inserire almeno un risultato per pagina");

            RuleFor(x => x.DataPubblicazione)
                .Must(x => x <= DateTime.Now || x==null)
                .WithMessage("La data di inserimento non può essere oltre quella attuale o deve essere nulla");
        }

    }
}
