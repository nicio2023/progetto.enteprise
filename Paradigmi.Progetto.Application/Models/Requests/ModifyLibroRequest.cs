using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Models.Requests
{
    public class ModifyLibroRequest
    {
        public string? Nome { get; set; }
        public string? Autore { get; set; }
        public string? NomeModificato { get; set; }
        public string? AutoreModificato { get; set; }
        public string? EditoreModificato { get; set; }
        public DateTime? DataPubblicazioneModificata { get; set; }
        public List<string>? CategorieModificate { get; set; }

    }
}
