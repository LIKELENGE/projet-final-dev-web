using System;

namespace Infrastructure.Models
{
    public class CommentaireAnnonce
    {
        public int IdCommentaireAnnonce { get; set; }

        public int IdAnnonce { get; set; }

        public int IdUtilisateur { get; set; }

        public string ContenuCommentaireAnnonce { get; set; } = string.Empty;

        public DateTime DateCommentaire { get; set; }
    }
}