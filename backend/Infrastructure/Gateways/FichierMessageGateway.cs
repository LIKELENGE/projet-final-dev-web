using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class FichierMessageGateway : IFichierMessageGateway
{
    private readonly IFichierMessageRepository _fichierMessageRepository;

    public FichierMessageGateway(IFichierMessageRepository fichierMessageRepository)
    {
        _fichierMessageRepository = fichierMessageRepository ?? throw new ArgumentNullException(nameof(fichierMessageRepository));
    }

    public IEnumerable<FichierMessage> GetAllFichiersMessage()
    {
        return _fichierMessageRepository.GetAllFichiersMessage();
    }

    public FichierMessage? GetFichierMessageById(int lienId)
    {
        return _fichierMessageRepository.GetFichierMessageById(lienId);
    }

    public IEnumerable<FichierMessage> GetFichiersByMessageId(int messageId)
    {
        return _fichierMessageRepository.GetFichiersByMessageId(messageId);
    }

    public void AddFichierMessage(FichierMessage fichierMessage)
    {
        _fichierMessageRepository.AddFichierMessage(fichierMessage);
    }

    public void DeleteFichierMessage(int lienId)
    {
        _fichierMessageRepository.DeleteFichierMessage(lienId);
    }
}
