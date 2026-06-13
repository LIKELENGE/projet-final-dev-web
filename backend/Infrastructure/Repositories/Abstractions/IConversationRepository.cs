using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IConversationRepository
    {
        IEnumerable<Conversation> GetAllConversations();
        Conversation? GetConversationById(int conversationId);
        void AddConversation(Conversation conversation);
        void UpdateConversation(Conversation conversation);
        void DeleteConversation(int conversationId);
    }
}