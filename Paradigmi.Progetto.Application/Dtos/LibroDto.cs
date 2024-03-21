using Paradigmi.Progetto.Models.Entities;

namespace Paradigmi.Progetto.Application.Dtos
{
    public class LibroDto
    {
        public int IdLibro { get;set; }
        public string Nome { get;set;}
        public string Autore { get;set;}
        public string Editore { get;set;}   
        //public ICollection<Categoria> Categorie { get; set; }
        public LibroDto(Libro libro)
        {
            IdLibro = libro.IdLibro;
            Nome = libro.Nome;
            Autore = libro.Autore;
            Editore = libro.Editore;
            //Categorie = libro.Categorie;
        }
    }
}
