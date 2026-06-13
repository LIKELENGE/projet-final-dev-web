using System;

namespace Infrastructure.Models
{
    public class Moderer
    {
        public int IdModeration { get; set; }

        public int Compte { get; set; }

        public int IdAnnonce { get; set; }

        public int IdEtat { get; set; }

        public DateTime DateStatut { get; set; }

        public DateTime? DelaiStatut { get; set; }

        public bool Illimite { get; set; }
    }
}