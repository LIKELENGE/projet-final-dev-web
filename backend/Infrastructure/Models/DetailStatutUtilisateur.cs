using System;

namespace Infrastructure.Models
{
    public class DetailStatutUtilisateur
    {
        public int IdDetailStatutUtilisateur { get; set; }

        public int IdUtilisateur { get; set; }

        public int IdStatutUtilisateur { get; set; }

        public DateTime DateStatut { get; set; }

        public DateTime? DelaiStatut { get; set; }

        public bool Illimite { get; set; }
    }
}