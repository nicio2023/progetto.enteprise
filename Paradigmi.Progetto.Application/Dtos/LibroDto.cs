using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Dtos
{
    public class LibroDto
    {
        public int IdLibro { get;set; }
        public string Nome { get;set;}
        public string Autore { get;set;}
        public string Editore { get;set;}   
        public ICollection<string> Categorie { get; set; }
        public LibroDto(Libro libro)
        {
            IdLibro = libro.IdLibro;
            Nome = libro.Nome;
            Autore = libro.Autore;
            Editore = libro.Editore;
            Categorie = GetNomeCategorie(libro.Categorie);
        }

        private ICollection<string> GetNomeCategorie(ICollection<CategoriaLibro> categorie)
        {
            List<string>? nomi = new List<string>();
            foreach(CategoriaLibro c in categorie)
            {
                nomi.Add(c.Categoria.Nome);
            }
            return nomi;
        }
    }
}
