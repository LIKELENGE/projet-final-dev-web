using Core.Models;

namespace Core.UseCases.Abstractions;

public interface IListerChoixUseCase
{
    IEnumerable<Categorie> GetCategories();
    IEnumerable<Commune> GetCommunes();
    IEnumerable<EtatAnnonce> GetEtatsAnnonce();
    IEnumerable<Sexe> GetSexes();
}
