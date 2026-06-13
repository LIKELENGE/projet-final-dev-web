using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

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
        return _etatProduitRepository.GetAllEtatsProduit();
    }

    public EtatProduit? GetEtatProduitById(int etatProduitId)
    {
        return _etatProduitRepository.GetEtatProduitById(etatProduitId);
    }

    public void AddEtatProduit(EtatProduit etatProduit)
    {
        _etatProduitRepository.AddEtatProduit(etatProduit);
    }

    public void UpdateEtatProduit(EtatProduit etatProduit)
    {
        _etatProduitRepository.UpdateEtatProduit(etatProduit);
    }

    public void DeleteEtatProduit(int etatProduitId)
    {
        _etatProduitRepository.DeleteEtatProduit(etatProduitId);
    }
}
