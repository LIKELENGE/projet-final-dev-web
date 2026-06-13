using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ModifierCategorieUseCase : IModifierCategorieUseCase
{
    private readonly ICategorieGateway _categorieGateway;

    public ModifierCategorieUseCase(ICategorieGateway categorieGateway)
    {
        _categorieGateway = categorieGateway ?? throw new ArgumentNullException(nameof(categorieGateway));
    }

    public void Execute(Categorie categorie)
    {
        ArgumentNullException.ThrowIfNull(categorie);

        if (categorie.Id <= 0)
        {
            throw new ArgumentException("L'identifiant de la categorie est invalide.", nameof(categorie));
        }

        if (string.IsNullOrWhiteSpace(categorie.Nom))
        {
            throw new ArgumentException("Le nom de la categorie est obligatoire.", nameof(categorie));
        }

        if (_categorieGateway.GetCategorieById(categorie.Id) == null)
        {
            throw new InvalidOperationException("La categorie a modifier est introuvable.");
        }

        _categorieGateway.UpdateCategorie(categorie);
    }
}
