using Paradigmi.Progetto.Application.Dtos;

namespace Paradigmi.Progetto.Application.Models.Responses
{
    public class GetLibriResponse
    {
        public List<LibroDto> Libri { get; set; } = new List<LibroDto>();
        public int NumeroPagine { get; set; }
    }
}
