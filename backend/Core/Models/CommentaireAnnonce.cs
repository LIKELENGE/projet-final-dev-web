namespace Core.Models;

public class CommentaireAnnonce
{
    public int Id { get; set; }

    public Annonce? Annonce { get; set; }

    public Utilisateur? Utilisateur { get; set; }

    public string Contenu { get; set; } = string.Empty;

    public DateTime DateCommentaire { get; set; }
}
