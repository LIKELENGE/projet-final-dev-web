using Core.IGateways;
using Infrastructure.models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class MessageGateway : IMessageGateway
{
    private readonly IMessageRepository _messageRepository;

    public MessageGateway(IMessageRepository messageRepository)
    {
        _messageRepository = messageRepository ?? throw new ArgumentNullException(nameof(messageRepository));
    }

    public IEnumerable<Message> GetAllMessages()
    {
        return _messageRepository.GetAllMessages();
    }

    public Message? GetMessageById(int messageId)
    {
        return _messageRepository.GetMessageById(messageId);
    }

    public IEnumerable<Message> GetMessagesByConversationId(int conversationId)
    {
        return _messageRepository.GetMessagesByConversationId(conversationId);
    }

    public void AddMessage(Message message)
    {
        _messageRepository.AddMessage(message);
    }

    public void UpdateMessage(Message message)
    {
        _messageRepository.UpdateMessage(message);
    }

    public void DeleteMessage(int messageId)
    {
        _messageRepository.DeleteMessage(messageId);
    }
}
