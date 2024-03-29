using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Paradigmi.Progetto.Application.RemoveSpaces;
using Paradigmi.Progetto.Models.Context;
using Paradigmi.Progetto.Models.Entities;
using Paradigmi.Progetto.Models.Repositories;
using System.Linq;

namespace Paradigmi.Progetto.Application.Models.Requests
{
    public class CreateLibroRequest
    {
        public string? Nome { get; set; } = string.Empty;
        public string? Autore { get; set; } = string.Empty;
        public string? Editore { get; set; } = string.Empty;
        public DateTime? DataPubblicazione { get; set; }
        public List<string>? Categorie { get; set; }


        public Libro ToEntity(List<CategoriaLibro> categorie)
        {
            var libro = new Libro();
            libro.Nome = Nome;
            libro.Editore = Editore;
            libro.Autore = Autore;
            libro.DataPubblicazione = DataPubblicazione;
            libro.Categorie = categorie;
            return libro;
        }
    }
}
