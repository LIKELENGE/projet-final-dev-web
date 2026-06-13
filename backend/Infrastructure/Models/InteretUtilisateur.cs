using System;

namespace Infrastructure.Models
{
    public class InteretUtilisateur
    {
        public int IdInteretUtilisateur { get; set; }

        public int IdUtilisateur { get; set; }

        public int IdCategorie { get; set; }

        public int? IdCommune { get; set; }

        public DateTime DateConsultation { get; set; }
    }
}