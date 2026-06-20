namespace Api.Models;

public class InscriptionRequest
{
    public string Nom { get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public string Mail { get; set; } = string.Empty;

    public string? Tel { get; set; }

    public string? PhotoProfil { get; set; }

    public string MotDePasse { get; set; } = string.Empty;

    public DateTime? DateNaissance { get; set; }

    public int? CodeSexe { get; set; }

    public int? CommuneId { get; set; }
}
