namespace Core.Models;

public class Annonce
{
    public int Id { get; set; }

    public Utilisateur? Utilisateur { get; set; }

    public Categorie? Categorie { get; set; }

    public Commune? Commune { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string? Description { get; set; }

    public double Prix { get; set; }

    public DateTime DateAjout { get; set; }

    public DateTime DerniereModification { get; set; }

    public List<PhotoAnnonce> Photos { get; set; } = new();

    public List<CommentaireAnnonce> Commentaires { get; set; } = new();
}
