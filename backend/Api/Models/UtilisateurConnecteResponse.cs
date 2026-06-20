namespace Api.Models;

public class UtilisateurConnecteResponse
{
    public int Id { get; set; }

    public string Nom { get; set; } = string.Empty;

    public string Prenom { get; set; } = string.Empty;

    public string Mail { get; set; } = string.Empty;

    public string Token { get; set; } = string.Empty;
}
