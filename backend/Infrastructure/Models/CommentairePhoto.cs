using System;

namespace Infrastructure.Models
{
    public class CommentairePhoto
    {
        public int IdCommentairePhoto { get; set; }

        public int IdPhotoAnnonce { get; set; }

        public int IdUtilisateur { get; set; }

        public string ContenuCommentairePhoto { get; set; } = string.Empty;

        public DateTime DateCommentaire { get; set; }
    }
}