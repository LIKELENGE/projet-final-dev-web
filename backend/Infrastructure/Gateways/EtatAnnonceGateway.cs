using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class EtatAnnonceGateway : IEtatAnnonceGateway
{
    private readonly IEtatAnnonceRepository _etatAnnonceRepository;

    public EtatAnnonceGateway(IEtatAnnonceRepository etatAnnonceRepository)
    {
        _etatAnnonceRepository = etatAnnonceRepository ?? throw new ArgumentNullException(nameof(etatAnnonceRepository));
    }

    public IEnumerable<EtatAnnonce> GetAllEtatsAnnonce()
    {
        return _etatAnnonceRepository.GetAllEtatsAnnonce().Select(etat => etat.ToCore());
    }

    public EtatAnnonce? GetEtatAnnonceById(int etatId)
    {
        var etat = _etatAnnonceRepository.GetEtatAnnonceById(etatId);

        return etat?.ToCore();
    }

    public void AddEtatAnnonce(EtatAnnonce etatAnnonce)
    {
        var etatDb = etatAnnonce.ToInfrastructure();
        _etatAnnonceRepository.AddEtatAnnonce(etatDb);
        etatAnnonce.Id = etatDb.IdEtat;
    }

    public void UpdateEtatAnnonce(EtatAnnonce etatAnnonce)
    {
        _etatAnnonceRepository.UpdateEtatAnnonce(etatAnnonce.ToInfrastructure());
    }

    public void DeleteEtatAnnonce(int etatId)
    {
        _etatAnnonceRepository.DeleteEtatAnnonce(etatId);
    }
}
