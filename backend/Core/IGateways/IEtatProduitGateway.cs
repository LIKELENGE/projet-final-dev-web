using Core.Models;

namespace Core.IGateways;

public interface IEtatProduitGateway
{
    IEnumerable<EtatProduit> GetAllEtatsProduit();
    EtatProduit? GetEtatProduitById(int etatProduitId);
    void AddEtatProduit(EtatProduit etatProduit);
    void UpdateEtatProduit(EtatProduit etatProduit);
    void DeleteEtatProduit(int etatProduitId);
}
