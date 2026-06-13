using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class ConversationGateway : IConversationGateway
{
    private readonly IConversationRepository _conversationRepository;

    public ConversationGateway(IConversationRepository conversationRepository)
    {
        _conversationRepository = conversationRepository ?? throw new ArgumentNullException(nameof(conversationRepository));
    }

    public IEnumerable<Conversation> GetAllConversations()
    {
        return _conversationRepository.GetAllConversations();
    }

    public Conversation? GetConversationById(int conversationId)
    {
        return _conversationRepository.GetConversationById(conversationId);
    }

    public void AddConversation(Conversation conversation)
    {
        _conversationRepository.AddConversation(conversation);
    }

    public void UpdateConversation(Conversation conversation)
    {
        _conversationRepository.UpdateConversation(conversation);
    }

    public void DeleteConversation(int conversationId)
    {
        _conversationRepository.DeleteConversation(conversationId);
    }
}
