using System;

namespace Infrastructure.Models
{
    public class FichierMessage
    {
        public int IdLien { get; set; }

        public int IdMessage { get; set; }

        public string Lien { get; set; } = string.Empty;
    }
}