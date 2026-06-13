using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class CreerCategorieUseCase : ICreerCategorieUseCase
{
    private readonly ICategorieGateway _categorieGateway;

    public CreerCategorieUseCase(ICategorieGateway categorieGateway)
    {
        _categorieGateway = categorieGateway ?? throw new ArgumentNullException(nameof(categorieGateway));
    }

    public void Execute(Categorie categorie)
    {
        ArgumentNullException.ThrowIfNull(categorie);

        if (string.IsNullOrWhiteSpace(categorie.Nom))
        {
            throw new ArgumentException("Le nom de la categorie est obligatoire.", nameof(categorie));
        }

        _categorieGateway.AddCategorie(categorie);
    }
}
