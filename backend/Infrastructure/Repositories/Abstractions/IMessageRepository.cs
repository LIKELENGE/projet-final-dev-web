using System.Collections.Generic;
using Infrastructure.models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IMessageRepository
    {
        IEnumerable<Message> GetAllMessages();
        Message? GetMessageById(int messageId);
        IEnumerable<Message> GetMessagesByConversationId(int conversationId);
        void AddMessage(Message message);
        void UpdateMessage(Message message);
        void DeleteMessage(int messageId);
    }
}
