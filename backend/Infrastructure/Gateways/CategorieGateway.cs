using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

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
        return _categorieRepository.GetAllCategories();
    }

    public Categorie? GetCategorieById(int categorieId)
    {
        return _categorieRepository.GetCategorieById(categorieId);
    }

    public void AddCategorie(Categorie categorie)
    {
        _categorieRepository.AddCategorie(categorie);
    }

    public void UpdateCategorie(Categorie categorie)
    {
        _categorieRepository.UpdateCategorie(categorie);
    }

    public void DeleteCategorie(int categorieId)
    {
        _categorieRepository.DeleteCategorie(categorieId);
    }
}
