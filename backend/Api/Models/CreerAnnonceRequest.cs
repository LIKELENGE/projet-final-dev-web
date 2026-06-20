namespace Api.Models;

public class CreerAnnonceRequest
{
    public int CategorieId { get; set; }

    public int? CommuneId { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string? Description { get; set; }

    public double Prix { get; set; }

    public List<PhotoAnnonceRequest> Photos { get; set; } = new();
}
