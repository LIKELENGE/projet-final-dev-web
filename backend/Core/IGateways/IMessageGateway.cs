using Core.Models;

namespace Core.IGateways;

public interface IMessageGateway
{
    IEnumerable<Message> GetAllMessages();
    Message? GetMessageById(int messageId);
    IEnumerable<Message> GetMessagesByConversationId(int conversationId);
    void AddMessage(Message message);
    void UpdateMessage(Message message);
    void DeleteMessage(int messageId);
}
