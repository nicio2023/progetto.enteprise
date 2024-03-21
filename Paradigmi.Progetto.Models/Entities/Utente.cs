using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paradigmi.Progetto.Models.Entities
{
    public class Utente
    {
        public int IdUtente { get; set; }
        public string Nome {get; set; } = string.Empty;
        public string Cognome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password {get; set; } = string.Empty;
    }
}
