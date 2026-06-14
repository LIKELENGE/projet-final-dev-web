namespace Api.Models;

public class ValiderAnnonceRequest
{
    public int AdminCompte { get; set; }

    public int AnnonceId { get; set; }

    public int EtatAnnonceId { get; set; }

    public DateTime? DelaiStatut { get; set; }

    public bool Illimite { get; set; }
}
