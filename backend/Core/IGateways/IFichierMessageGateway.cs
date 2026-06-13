using Core.Models;

namespace Core.IGateways;

public interface IFichierMessageGateway
{
    IEnumerable<FichierMessage> GetAllFichiersMessage();
    FichierMessage? GetFichierMessageById(int lienId);
    IEnumerable<FichierMessage> GetFichiersByMessageId(int messageId);
    void AddFichierMessage(FichierMessage fichierMessage);
    void DeleteFichierMessage(int lienId);
}
