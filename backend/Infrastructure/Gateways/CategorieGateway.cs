using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class CategorieGateway : ICategorieGateway
{
    private readonly ICategorieRepository _categorieRepository;

    public CategorieGateway(ICategorieRepository categorieRepository)
    {
        _categorieRepository = categorieRepository ?? throw new ArgumentNullException(nameof(categorieRepository));
    }

    public IEnumerable<Categorie> GetAllCategories()
    {
        return _categorieRepository.GetAllCategories().Select(categorie => categorie.ToCore());
    }

    public Categorie? GetCategorieById(int categorieId)
    {
        var categorie = _categorieRepository.GetCategorieById(categorieId);

        return categorie?.ToCore();
    }

    public void AddCategorie(Categorie categorie)
    {
        var categorieDb = categorie.ToInfrastructure();
        _categorieRepository.AddCategorie(categorieDb);
        categorie.Id = categorieDb.IdCategorie;
    }

    public void UpdateCategorie(Categorie categorie)
    {
        _categorieRepository.UpdateCategorie(categorie.ToInfrastructure());
    }

    public void DeleteCategorie(int categorieId)
    {
        _categorieRepository.DeleteCategorie(categorieId);
    }
}
