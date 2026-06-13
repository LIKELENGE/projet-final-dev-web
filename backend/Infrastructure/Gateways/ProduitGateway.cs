using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class ProduitGateway : IProduitGateway
{
    private readonly IProduitRepository _produitRepository;

    public ProduitGateway(IProduitRepository produitRepository)
    {
        _produitRepository = produitRepository ?? throw new ArgumentNullException(nameof(produitRepository));
    }

    public IEnumerable<Produit> GetAllProduits()
    {
        return _produitRepository.GetAllProduits();
    }

    public Produit? GetProduitByAnnonceId(int annonceId)
    {
        return _produitRepository.GetProduitByAnnonceId(annonceId);
    }

    public void AddProduit(Produit produit)
    {
        _produitRepository.AddProduit(produit);
    }

    public void UpdateProduit(Produit produit)
    {
        _produitRepository.UpdateProduit(produit);
    }

    public void DeleteProduit(int annonceId)
    {
        _produitRepository.DeleteProduit(annonceId);
    }
}
