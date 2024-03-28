namespace Paradigmi.Progetto.Application.Models.Requests
{
    public class GetLibriRequest
    {
        public int PageSize { get; set; } 
        public int PageNumber { get; set; } 
        public string? Nome { get; set; } = string.Empty;
        public string? Autore { get; set; } = string.Empty;
        public DateTime? DataPubblicazione { get; set; }
        public string? Categoria { get; set; } = string.Empty;
    }
}
