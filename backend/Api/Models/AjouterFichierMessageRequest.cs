namespace Api.Models;

public class AjouterFichierMessageRequest
{
    public int MessageId { get; set; }

    public string Lien { get; set; } = string.Empty;
}
