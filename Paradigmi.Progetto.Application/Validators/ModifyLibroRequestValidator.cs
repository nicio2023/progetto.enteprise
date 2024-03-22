using FluentValidation;
using Microsoft.Graph.Drives.Item.Items.Item.Workbook.Functions.Nominal;
using Paradigmi.Progetto.Application.Requests;
using Paradigmi.Progetto.Models.Entities;
using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class ModifyLibroRequestValidator : AbstractValidator<ModifyLibroRequest>
    {
        private readonly LibroRepository _libroRepository;
        private readonly CategoriaRepository _categoriaRepository;

        public ModifyLibroRequestValidator(LibroRepository libroRepository, CategoriaRepository categoriaRepository)
        {
            _libroRepository = libroRepository;
            _categoriaRepository = categoriaRepository;


            RuleFor(x => x)
                .Must(entity => _libroRepository.GetLibroByNomeEAutore(entity.Nome.ToLower(), entity.Autore.ToLower()) != -1)
                .WithMessage("libro non esistente");
            
            RuleFor(x => x.AutoreModificato)
                .Custom(ValidaModificaLibro);

            RuleFor(x => x.EditoreModificato)
                .Custom(ValidaModificaLibro);

            RuleForEach(x => x.CategorieModificate)
                .Custom(ValidaCategorieLibro);

            RuleFor(x => x)
                .Custom(VerificaModificaLibro);

        }

        private void ValidaModificaLibro(string value, ValidationContext<ModifyLibroRequest> context)
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
        private void ValidaCategorieLibro(string value, ValidationContext<ModifyLibroRequest> context)
        {
            if (_categoriaRepository.GetCategoriaByNome(value.ToLower()) == -1)
            {
                context.AddFailure("il campo <" + value + "> non esiste tra le categorie. Per accettare l'inserimento" +
                    " tutte le categorie inserite devono essere presenti.");
            }
        }
        private void VerificaModificaLibro(ModifyLibroRequest request, ValidationContext<ModifyLibroRequest> context)
        {
            var index = _libroRepository.GetLibroByNomeEAutore(request.Nome, request.Autore);
            var libro = _libroRepository.GetLibroById(index);
            List<string> Nomi = new List<string>();
            foreach(CategoriaLibro c in libro.Categorie)
            {
                Nomi.Add(c.Categoria.Nome);
            }

            List<string> NomiModificati = new List<string>();
            foreach(string c in request.CategorieModificate)
            {
                NomiModificati.Add(c);
            }

            bool valid = VerificaUguaglianzaNomi(Nomi, NomiModificati);

            if((libro.Autore == request.AutoreModificato) && (libro.Editore == request.EditoreModificato)
                && (libro.DataPubblicazione == request.DataPubblicazioneModificata) && valid)
            {
                context.AddFailure("almeno un campo tra autore, editore, la data della pubblicazione e i nomi delle categorie " +
                    "deve cambiare");
            }

        }
        private bool VerificaUguaglianzaNomi(List<string> Nomi, List<string> NomiModificati)
        {
            return Nomi.SequenceEqual(NomiModificati);
        }
    }
}
