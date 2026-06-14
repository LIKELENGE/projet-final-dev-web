using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _fichierMessageRepository.GetAllFichiersMessage().Select(fichier => fichier.ToCore());
    }

    public FichierMessage? GetFichierMessageById(int lienId)
    {
        var fichier = _fichierMessageRepository.GetFichierMessageById(lienId);

        return fichier?.ToCore();
    }

    public IEnumerable<FichierMessage> GetFichiersByMessageId(int messageId)
    {
        return _fichierMessageRepository.GetFichiersByMessageId(messageId).Select(fichier => fichier.ToCore());
    }

    public void AddFichierMessage(FichierMessage fichierMessage)
    {
        var fichierDb = fichierMessage.ToInfrastructure();
        _fichierMessageRepository.AddFichierMessage(fichierDb);
        fichierMessage.Id = fichierDb.IdLien;
    }

    public void DeleteFichierMessage(int lienId)
    {
        _fichierMessageRepository.DeleteFichierMessage(lienId);
    }
}
