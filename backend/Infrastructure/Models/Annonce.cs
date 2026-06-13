using System;

namespace Infrastructure.Models
{
    public class Annonce
    {
        public int IdAnnonce { get; set; }

        public int IdUtilisateur { get; set; }

        public int IdCategorie { get; set; }

        public int? IdCommune { get; set; }

        public string Nom { get; set; } = string.Empty;

        public string? Description { get; set; }

        public double Prix { get; set; }

        public DateTime DateAjout { get; set; }

        public DateTime DerniereModification { get; set; }
    }
}