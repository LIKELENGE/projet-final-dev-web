using System;

namespace Infrastructure.Models
{
    public class Utilisateur
    {
        public int IdUtilisateur { get; set; }

        public string Nom { get; set; } = string.Empty;

        public string Prenom { get; set; } = string.Empty;

        public string Mail { get; set; } = string.Empty;

        public string? Tel { get; set; }

        public string? PhotoProfil { get; set; }

        public string Mp { get; set; } = string.Empty;

        public DateTime DateInscription { get; set; }

        public DateTime? DateNaiss { get; set; }

        public int? CodeSexe { get; set; }

        public int? IdCommune { get; set; }
    }
}