namespace Core.Models;

public class Dimension
{
    public int Id { get; set; }

    public Annonce? Annonce { get; set; }

    public double? ProfondeurCm { get; set; }

    public double? LongueurCm { get; set; }

    public double? LargeurCm { get; set; }

    public double? PoidsKg { get; set; }
}
