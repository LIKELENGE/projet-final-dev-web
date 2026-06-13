using Core.Models;

namespace Core.IGateways;

public interface ICategorieGateway
{
    IEnumerable<Categorie> GetAllCategories();
    Categorie? GetCategorieById(int categorieId);
    void AddCategorie(Categorie categorie);
    void UpdateCategorie(Categorie categorie);
    void DeleteCategorie(int categorieId);
}
