using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

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
        return _etatAnnonceRepository.GetAllEtatsAnnonce();
    }

    public EtatAnnonce? GetEtatAnnonceById(int etatId)
    {
        return _etatAnnonceRepository.GetEtatAnnonceById(etatId);
    }

    public void AddEtatAnnonce(EtatAnnonce etatAnnonce)
    {
        _etatAnnonceRepository.AddEtatAnnonce(etatAnnonce);
    }

    public void UpdateEtatAnnonce(EtatAnnonce etatAnnonce)
    {
        _etatAnnonceRepository.UpdateEtatAnnonce(etatAnnonce);
    }

    public void DeleteEtatAnnonce(int etatId)
    {
        _etatAnnonceRepository.DeleteEtatAnnonce(etatId);
    }
}
