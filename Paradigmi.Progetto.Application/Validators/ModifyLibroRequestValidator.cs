using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Functions.Nominal;
using Paradigmi.Progetto.Application.Models.Requests;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Abstractions;
using Paradigmi.Progetto.Models.Entities;
using Paradigmi.Progetto.Models.Repositories;
using System.Collections;

namespace Paradigmi.Progetto.Application.Validators
{
    public class ModifyLibroRequestValidator : AbstractValidator<ModifyLibroRequest>
    {
        private readonly ILibroRepository _libroRepository;
        private readonly ICategoriaRepository _categoriaRepository;

        public ModifyLibroRequestValidator(ILibroRepository libroRepository, ICategoriaRepository categoriaRepository)
        {
            _libroRepository = libroRepository;
            _categoriaRepository = categoriaRepository;

            RuleFor(x => x.Nome)
                 .NotNull()
                 .WithMessage("il campo nome non può essere nulllo")
                 .MinimumLength(1)
                 .WithMessage("il campo nome deve avere almeno un carattere");

            RuleFor(x => x.NomeModificato)
                .NotNull()
                 .WithMessage("il campo nome modificato non può essere nulllo")
                 .MinimumLength(1)
                 .WithMessage("il campo libro modificato deve avere almeno un carattere");

            
            RuleFor(x => x.Autore)
                .Custom(ValidaModificaLibro);
            
            RuleFor(x => x.AutoreModificato)
                .Custom(ValidaModificaLibro);

            RuleFor(x => x.EditoreModificato)
                .Custom(ValidaModificaLibro);

            RuleFor(x => x)
              .Must(entity => _libroRepository.GetIndiceLibroByNomeEAutore(Spaces.RemoveExtraSpaces(entity.Nome?.ToLower()), Spaces.RemoveExtraSpaces(entity.Autore?.ToLower())) != -1)
              .WithMessage("libro non esistente");

            RuleFor(x => x)
               .Custom(VerificaModificaLibro);
              
            RuleFor(x => x.DataPubblicazioneModificata)
                .NotNull()
                .WithMessage("il campo data di pubblicazione non può essere nullo")
                .Must(x => x <= DateTime.Now)
                .WithMessage("La data di inserimento non può essere oltre quella attuale");

            RuleFor(x => x.CategorieModificate)
                .Must(x => x != null)
                .WithMessage("la lista di categorie non può essere nulla");

            RuleForEach(x => x.CategorieModificate)
                .Custom(ValidaCategorieLibro);


        }

        private void ValidaModificaLibro(string? value, ValidationContext<ModifyLibroRequest> context)
        {
            if (value == null)
            {
                context.AddFailure("i campi autore, autore modificato e editore modificato non possono essere nulli");
            }
            else if (value.Length < 3)
            {
                context.AddFailure("i campi autore, autore modificato e editore modificato non possono essere nulli");
            }
        }
        private void ValidaCategorieLibro(string? value, ValidationContext<ModifyLibroRequest> context)
        {
            if (_categoriaRepository.GetCategoriaIndiceByNome(Spaces.RemoveExtraSpaces(value?.ToLower())) == -1)
            {
                context.AddFailure("il campo <" + value + "> non esiste tra le categorie. Per accettare l'inserimento" +
                    " tutte le categorie inserite devono essere presenti.");
            }
        }
        private void VerificaModificaLibro(ModifyLibroRequest request, ValidationContext<ModifyLibroRequest> context)
        {
            string nome = Spaces.RemoveExtraSpaces(request.Nome);
            string autore = Spaces.RemoveExtraSpaces(request.Autore);
            var index = _libroRepository.GetIndiceLibroByNomeEAutore(nome?.ToLower(), autore?.ToLower());
            var libro = _libroRepository.Ottieni(index);
           
            if (libro != null)
            {
                var Nomi = new HashSet<string>(libro.Categorie.Select(x => x.Categoria.Nome.ToLower()));


                HashSet<string>? NomiModificati = new HashSet<string>();
                if (request.CategorieModificate != null)
                {
                    NomiModificati =new HashSet<string> (request.CategorieModificate.Select(x => Spaces.RemoveExtraSpaces(x.ToLower())).ToList());
                }


                bool valid = VerificaUguaglianzaNomi(Nomi, NomiModificati);

                if ((libro.Nome.ToLower() == Spaces.RemoveExtraSpaces(request.NomeModificato?.ToLower())) && 
                    (libro.Autore.ToLower() == Spaces.RemoveExtraSpaces(request.AutoreModificato?.ToLower())) && 
                    (libro.Editore.ToLower() == Spaces.RemoveExtraSpaces(request.EditoreModificato?.ToLower())
                    && (libro.DataPubblicazione == request.DataPubblicazioneModificata) && valid))
                {
                    context.AddFailure("almeno un campo tra nome, autore, editore, la data della pubblicazione e i nomi delle categorie " +
                        "deve cambiare");
                }
            }

        }
        private bool VerificaUguaglianzaNomi(HashSet<string>? Nomi, HashSet<string>? NomiModificati)
        {
            if (NomiModificati != null) {
                return Nomi.SetEquals(NomiModificati);
            }
            else
            {
                return false;
            }
        }
    }
}
