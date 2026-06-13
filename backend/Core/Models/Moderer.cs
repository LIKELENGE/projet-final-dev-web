namespace Core.Models;

public class Moderer
{
    public int Id { get; set; }

    public Admin? Admin { get; set; }

    public Annonce? Annonce { get; set; }

    public EtatAnnonce? EtatAnnonce { get; set; }

    public DateTime DateStatut { get; set; }

    public DateTime? DelaiStatut { get; set; }

    public bool Illimite { get; set; }
}
