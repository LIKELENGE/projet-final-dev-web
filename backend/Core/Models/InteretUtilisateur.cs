namespace Core.Models;

public class InteretUtilisateur
{
    public int Id { get; set; }

    public Utilisateur? Utilisateur { get; set; }

    public Categorie? Categorie { get; set; }

    public Commune? Commune { get; set; }

    public DateTime DateConsultation { get; set; }
}
