using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ChatterUseCase : IChatterUseCase
{
    private readonly IConversationGateway _conversationGateway;
    private readonly IMessageGateway _messageGateway;
    private readonly IFichierMessageGateway _fichierMessageGateway;

    public ChatterUseCase(
        IConversationGateway conversationGateway,
        IMessageGateway messageGateway,
        IFichierMessageGateway fichierMessageGateway)
    {
        _conversationGateway = conversationGateway ?? throw new ArgumentNullException(nameof(conversationGateway));
        _messageGateway = messageGateway ?? throw new ArgumentNullException(nameof(messageGateway));
        _fichierMessageGateway = fichierMessageGateway ?? throw new ArgumentNullException(nameof(fichierMessageGateway));
    }

    public void CreerConversation(Conversation conversation)
    {
        ArgumentNullException.ThrowIfNull(conversation);

        _conversationGateway.AddConversation(conversation);
    }

    public IEnumerable<Conversation> GetConversations()
    {
        return _conversationGateway.GetAllConversations();
    }

    public Conversation? GetConversation(int conversationId)
    {
        if (conversationId <= 0)
        {
            throw new ArgumentException("L'identifiant de la conversation est invalide.", nameof(conversationId));
        }

        return _conversationGateway.GetConversationById(conversationId);
    }

    public IEnumerable<Message> GetMessages(int conversationId)
    {
        if (conversationId <= 0)
        {
            throw new ArgumentException("L'identifiant de la conversation est invalide.", nameof(conversationId));
        }

        return _messageGateway.GetMessagesByConversationId(conversationId);
    }

    public void EnvoyerMessage(Message message)
    {
        ArgumentNullException.ThrowIfNull(message);

        if (message.Conversation == null || message.Conversation.Id <= 0)
        {
            throw new ArgumentException("Le message doit etre lie a une conversation.", nameof(message));
        }

        if (message.Utilisateur == null || message.Utilisateur.Id <= 0)
        {
            throw new ArgumentException("Le message doit etre lie a un utilisateur.", nameof(message));
        }

        if (string.IsNullOrWhiteSpace(message.Contenu))
        {
            throw new ArgumentException("Le contenu du message est obligatoire.", nameof(message));
        }

        if (message.DateHeureMessage == default)
        {
            message.DateHeureMessage = DateTime.UtcNow;
        }

        _messageGateway.AddMessage(message);
    }

    public void AjouterFichierMessage(FichierMessage fichierMessage)
    {
        ArgumentNullException.ThrowIfNull(fichierMessage);

        if (fichierMessage.Message == null || fichierMessage.Message.Id <= 0)
        {
            throw new ArgumentException("Le fichier doit etre lie a un message.", nameof(fichierMessage));
        }

        if (string.IsNullOrWhiteSpace(fichierMessage.Lien))
        {
            throw new ArgumentException("Le lien du fichier est obligatoire.", nameof(fichierMessage));
        }

        _fichierMessageGateway.AddFichierMessage(fichierMessage);
    }
}
