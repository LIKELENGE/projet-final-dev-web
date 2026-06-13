namespace Core.Models;

public class Admin
{
    public int Compte { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public string? Niveau { get; set; }

    public string MotDePasseHash { get; set; } = string.Empty;
}
