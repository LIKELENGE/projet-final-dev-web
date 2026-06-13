namespace Core.Models;

public class PhotoAnnonce
{
    public int Id { get; set; }

    public Annonce? Annonce { get; set; }

    public string? Titre { get; set; }

    public string Lien { get; set; } = string.Empty;

    public List<CommentairePhoto> Commentaires { get; set; } = new();
}
