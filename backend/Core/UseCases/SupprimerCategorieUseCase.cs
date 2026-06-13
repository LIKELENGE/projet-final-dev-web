using Core.IGateways;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class SupprimerCategorieUseCase : ISupprimerCategorieUseCase
{
    private readonly ICategorieGateway _categorieGateway;

    public SupprimerCategorieUseCase(ICategorieGateway categorieGateway)
    {
        _categorieGateway = categorieGateway ?? throw new ArgumentNullException(nameof(categorieGateway));
    }

    public void Execute(int categorieId)
    {
        if (categorieId <= 0)
        {
            throw new ArgumentException("L'identifiant de la categorie est invalide.", nameof(categorieId));
        }

        if (_categorieGateway.GetCategorieById(categorieId) == null)
        {
            throw new InvalidOperationException("La categorie a supprimer est introuvable.");
        }

        _categorieGateway.DeleteCategorie(categorieId);
    }
}
