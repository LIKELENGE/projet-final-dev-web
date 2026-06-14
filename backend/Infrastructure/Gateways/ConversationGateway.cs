using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _conversationRepository.GetAllConversations().Select(conversation => conversation.ToCore());
    }

    public Conversation? GetConversationById(int conversationId)
    {
        var conversation = _conversationRepository.GetConversationById(conversationId);

        return conversation?.ToCore();
    }

    public void AddConversation(Conversation conversation)
    {
        var conversationDb = conversation.ToInfrastructure();
        _conversationRepository.AddConversation(conversationDb);
        conversation.Id = conversationDb.IdConversation;
    }

    public void UpdateConversation(Conversation conversation)
    {
        _conversationRepository.UpdateConversation(conversation.ToInfrastructure());
    }

    public void DeleteConversation(int conversationId)
    {
        _conversationRepository.DeleteConversation(conversationId);
    }
}
