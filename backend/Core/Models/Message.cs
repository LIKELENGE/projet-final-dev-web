namespace Core.Models;

public class Message
{
    public int Id { get; set; }

    public Conversation? Conversation { get; set; }

    public Utilisateur? Utilisateur { get; set; }

    public string Contenu { get; set; } = string.Empty;

    public DateTime DateHeureMessage { get; set; }

    public List<FichierMessage> Fichiers { get; set; } = new();
}
