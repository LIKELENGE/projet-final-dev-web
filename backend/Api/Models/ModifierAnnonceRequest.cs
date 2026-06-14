namespace Api.Models;

public class ModifierAnnonceRequest
{
    public int UtilisateurId { get; set; }

    public int CategorieId { get; set; }

    public int? CommuneId { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string? Description { get; set; }

    public double Prix { get; set; }
}
