using Core.Models;

namespace Core.IGateways;

public interface IConversationGateway
{
    IEnumerable<Conversation> GetAllConversations();
    Conversation? GetConversationById(int conversationId);
    void AddConversation(Conversation conversation);
    void UpdateConversation(Conversation conversation);
    void DeleteConversation(int conversationId);
}
