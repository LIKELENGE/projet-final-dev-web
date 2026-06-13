using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IProduitRepository
    {
        IEnumerable<Produit> GetAllProduits();
        Produit? GetProduitByAnnonceId(int annonceId);
        void AddProduit(Produit produit);
        void UpdateProduit(Produit produit);
        void DeleteProduit(int annonceId);
    }
}