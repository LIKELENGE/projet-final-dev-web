using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ListerChoixUseCase : IListerChoixUseCase
{
    private readonly ICategorieGateway _categorieGateway;
    private readonly ICommuneGateway _communeGateway;
    private readonly ISexeGateway _sexeGateway;

    public ListerChoixUseCase(
        ICategorieGateway categorieGateway,
        ICommuneGateway communeGateway,
        ISexeGateway sexeGateway)
    {
        _categorieGateway = categorieGateway ?? throw new ArgumentNullException(nameof(categorieGateway));
        _communeGateway = communeGateway ?? throw new ArgumentNullException(nameof(communeGateway));
        _sexeGateway = sexeGateway ?? throw new ArgumentNullException(nameof(sexeGateway));
    }

    public IEnumerable<Categorie> GetCategories()
    {
        return _categorieGateway.GetAllCategories();
    }

    public IEnumerable<Commune> GetCommunes()
    {
        return _communeGateway.GetAllCommunes();
    }

    public IEnumerable<Sexe> GetSexes()
    {
        return _sexeGateway.GetAllSexes();
    }
}
