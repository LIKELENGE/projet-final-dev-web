using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IEtatProduitRepository
    {
        IEnumerable<EtatProduit> GetAllEtatsProduit();
        EtatProduit? GetEtatProduitById(int etatProduitId);
        void AddEtatProduit(EtatProduit etatProduit);
        void UpdateEtatProduit(EtatProduit etatProduit);
        void DeleteEtatProduit(int etatProduitId);
    }
}