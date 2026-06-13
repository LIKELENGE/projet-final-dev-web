using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IFichierMessageRepository
    {
        IEnumerable<FichierMessage> GetAllFichiersMessage();
        FichierMessage? GetFichierMessageById(int lienId);
        IEnumerable<FichierMessage> GetFichiersByMessageId(int messageId);
        void AddFichierMessage(FichierMessage fichierMessage);
        void DeleteFichierMessage(int lienId);
    }
}