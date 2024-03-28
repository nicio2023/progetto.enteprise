using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Models.Requests
{
    public class CreateCategoriaRequest
    {
        public string? Nome { get; set; } = string.Empty;
        public Categoria ToEntity()
        {
            var request = new Categoria();
            request.Nome = Spaces.RemoveExtraSpaces(Nome);
            return request;
        }
    }
}
