using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Dtos
{
    public class LibroDto
    {
        public int IdLibro { get;set; }
        public string Nome { get; set; } = string.Empty;
        public string Autore { get; set; } = string.Empty;
        public string Editore { get; set; } = string.Empty;
        public DateTime? DataPubblicazione { get; set; }
        public ICollection<string> Categorie { get; set; }
        public LibroDto(Libro libro)
        {
            IdLibro = libro.IdLibro;
            Nome = libro.Nome;
            Autore = libro.Autore;
            Editore = libro.Editore;
            DataPubblicazione = libro.DataPubblicazione;
            Categorie = GetNomeCategorie(libro.Categorie);
        }

        private ICollection<string> GetNomeCategorie(ICollection<CategoriaLibro>? categorie)
        {
            /*List<string>? nomi = new List<string>();
            foreach(CategoriaLibro c in categorie)
            {
                string nome = c.Categoria.Nome;
                nomi.Add(nome);
            }
            return nomi;*/
            List<string> nomi = categorie.Select(x => x.Categoria.Nome).ToList();
            return nomi;
        }
    }
}
