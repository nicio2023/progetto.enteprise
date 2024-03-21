using Paradigmi.Progetto.Models.Repositories;

namespace Paradigmi.Progetto.Application.Validators
{
    public class DeleteCategoriaRequestValidator
    {
        private readonly LibroRepository _libroRepository;
        public DeleteCategoriaRequestValidator(LibroRepository libroRepository)
        {
            _libroRepository = libroRepository;
        }
    }
}
