using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _messageRepository.GetAllMessages().Select(message => message.ToCore());
    }

    public Message? GetMessageById(int messageId)
    {
        var message = _messageRepository.GetMessageById(messageId);

        return message?.ToCore();
    }

    public IEnumerable<Message> GetMessagesByConversationId(int conversationId)
    {
        return _messageRepository.GetMessagesByConversationId(conversationId).Select(message => message.ToCore());
    }

    public void AddMessage(Message message)
    {
        var messageDb = message.ToInfrastructure();
        _messageRepository.AddMessage(messageDb);
        message.Id = messageDb.IdMessage;
    }

    public void UpdateMessage(Message message)
    {
        _messageRepository.UpdateMessage(message.ToInfrastructure());
    }

    public void DeleteMessage(int messageId)
    {
        _messageRepository.DeleteMessage(messageId);
    }
}
