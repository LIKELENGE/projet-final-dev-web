namespace Core.Models;

public class Commune
{
    public int Id { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string? CodePostal { get; set; }
}
