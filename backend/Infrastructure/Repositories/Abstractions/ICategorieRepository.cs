using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface ICategorieRepository
    {
        IEnumerable<Categorie> GetAllCategories();
        Categorie? GetCategorieById(int categorieId);
        void AddCategorie(Categorie categorie);
        void UpdateCategorie(Categorie categorie);
        void DeleteCategorie(int categorieId);
    }
}
