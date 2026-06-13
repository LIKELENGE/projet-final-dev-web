namespace Core.Models;

public class CommentairePhoto
{
    public int Id { get; set; }

    public PhotoAnnonce? PhotoAnnonce { get; set; }

    public Utilisateur? Utilisateur { get; set; }

    public string Contenu { get; set; } = string.Empty;

    public DateTime DateCommentaire { get; set; }
}
