using Core.Models;

namespace Core.IGateways;

public interface IEtatAnnonceGateway
{
    IEnumerable<EtatAnnonce> GetAllEtatsAnnonce();
    EtatAnnonce? GetEtatAnnonceById(int etatId);
    void AddEtatAnnonce(EtatAnnonce etatAnnonce);
    void UpdateEtatAnnonce(EtatAnnonce etatAnnonce);
    void DeleteEtatAnnonce(int etatId);
}
