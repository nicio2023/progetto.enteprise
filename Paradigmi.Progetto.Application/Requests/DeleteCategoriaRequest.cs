using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Requests
{
    public class DeleteCategoriaRequest
    {
        public string Nome { get; set; }
        public Categoria ToEntity()
        {
            var request = new Categoria();
            request.Nome = Nome;
            return request;
        }
    }
}
