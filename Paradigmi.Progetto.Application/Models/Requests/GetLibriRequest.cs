namespace Paradigmi.Progetto.Application.Models.Requests
{
    public class GetLibriRequest
    {
        public int PageSize { get; set; } //Rappresenta la grandezza della pagina
        public int PageNumber { get; set; } //Identifica il numero della pagina ad indice 0
        public string Nome { get; set; }
        public string? Autore { get; set; } 
        public DateTime? DataPubblicazione { get; set; }
        public string? Categoria { get; set; } 
    }
}
