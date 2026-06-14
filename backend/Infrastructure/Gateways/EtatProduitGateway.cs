using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class EtatProduitGateway : IEtatProduitGateway
{
    private readonly IEtatProduitRepository _etatProduitRepository;

    public EtatProduitGateway(IEtatProduitRepository etatProduitRepository)
    {
        _etatProduitRepository = etatProduitRepository ?? throw new ArgumentNullException(nameof(etatProduitRepository));
    }

    public IEnumerable<EtatProduit> GetAllEtatsProduit()
    {
        return _etatProduitRepository.GetAllEtatsProduit().Select(etat => etat.ToCore());
    }

    public EtatProduit? GetEtatProduitById(int etatProduitId)
    {
        var etat = _etatProduitRepository.GetEtatProduitById(etatProduitId);

        return etat?.ToCore();
    }

    public void AddEtatProduit(EtatProduit etatProduit)
    {
        var etatDb = etatProduit.ToInfrastructure();
        _etatProduitRepository.AddEtatProduit(etatDb);
        etatProduit.Id = etatDb.IdEtatProduit;
    }

    public void UpdateEtatProduit(EtatProduit etatProduit)
    {
        _etatProduitRepository.UpdateEtatProduit(etatProduit.ToInfrastructure());
    }

    public void DeleteEtatProduit(int etatProduitId)
    {
        _etatProduitRepository.DeleteEtatProduit(etatProduitId);
    }
}
