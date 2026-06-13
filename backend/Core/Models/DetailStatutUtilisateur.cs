namespace Core.Models;

public class DetailStatutUtilisateur
{
    public int Id { get; set; }

    public Utilisateur? Utilisateur { get; set; }

    public StatutUtilisateur? StatutUtilisateur { get; set; }

    public DateTime DateStatut { get; set; }

    public DateTime? DelaiStatut { get; set; }

    public bool Illimite { get; set; }
}
