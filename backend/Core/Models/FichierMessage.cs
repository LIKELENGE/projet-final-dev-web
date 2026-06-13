namespace Core.Models;

public class FichierMessage
{
    public int Id { get; set; }

    public Message? Message { get; set; }

    public string Lien { get; set; } = string.Empty;
}
