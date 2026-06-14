using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _produitRepository.GetAllProduits().Select(produit => produit.ToCore());
    }

    public Produit? GetProduitByAnnonceId(int annonceId)
    {
        var produit = _produitRepository.GetProduitByAnnonceId(annonceId);

        return produit?.ToCore();
    }

    public void AddProduit(Produit produit)
    {
        _produitRepository.AddProduit(produit.ToInfrastructure());
    }

    public void UpdateProduit(Produit produit)
    {
        _produitRepository.UpdateProduit(produit.ToInfrastructure());
    }

    public void DeleteProduit(int annonceId)
    {
        _produitRepository.DeleteProduit(annonceId);
    }
}
