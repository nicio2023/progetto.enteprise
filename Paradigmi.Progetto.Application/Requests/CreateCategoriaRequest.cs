using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Requests
{
    public class CreateCategoriaRequest
    {
        public string Nome { get; set; } = string.Empty;
        public Categoria ToEntity()
        {
            var request = new Categoria();
            request.Nome = Nome;
            return request;
        }
    }
}
