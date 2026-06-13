using Infrastructure.models;
using Infrastructure.Models;
using System.Collections.Generic;

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