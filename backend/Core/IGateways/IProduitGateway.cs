using Core.Models;

namespace Core.IGateways;

public interface IProduitGateway
{
    IEnumerable<Produit> GetAllProduits();
    Produit? GetProduitByAnnonceId(int annonceId);
    void AddProduit(Produit produit);
    void UpdateProduit(Produit produit);
    void DeleteProduit(int annonceId);
}
