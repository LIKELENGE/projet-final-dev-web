using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IChatterUseCase
{
    void CreerConversation(Conversation conversation);
    IEnumerable<Conversation> GetConversations();
    Conversation? GetConversation(int conversationId);
    IEnumerable<Message> GetMessages(int conversationId);
    void EnvoyerMessage(Message message);
    void AjouterFichierMessage(FichierMessage fichierMessage);
}
