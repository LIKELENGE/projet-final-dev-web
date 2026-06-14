namespace Api.Models;

public class EnvoyerMessageRequest
{
    public int ConversationId { get; set; }

    public int UtilisateurId { get; set; }

    public string Contenu { get; set; } = string.Empty;
}
