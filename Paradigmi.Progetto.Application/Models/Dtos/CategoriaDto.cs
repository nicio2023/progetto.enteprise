using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Dtos
{
    public class CategoriaDto
    {
        public int IdCategoria { get; set; }
        public string Nome { get; set; } = string.Empty;
        public CategoriaDto()
        {

        }
        public CategoriaDto(Categoria categoria)
        {
            IdCategoria = categoria.IdCategoria;
            Nome = categoria.Nome;
        }
    }
}
