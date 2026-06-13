namespace Core.Models;

public class Conversation
{
    public int Id { get; set; }

    public string? Titre { get; set; }

    public string? LienPhoto { get; set; }

    public List<Message> Messages { get; set; } = new();
}
