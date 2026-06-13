namespace Core.Models;

public class Utilisateur
{
    public int Id { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public string Mail { get; set; } = string.Empty;

    public string? Tel { get; set; }

    public string? PhotoProfil { get; set; }

    public string MotDePasseHash { get; set; } = string.Empty;

    public DateTime DateInscription { get; set; }

    public DateTime? DateNaissance { get; set; }

    public Sexe? Sexe { get; set; }

    public Commune? Commune { get; set; }

    public List<Annonce> Annonces { get; set; } = new();

    public List<InteretUtilisateur> Interets { get; set; } = new();
}
