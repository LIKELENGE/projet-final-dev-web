using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class RechercherAnnonceUseCase : IRechercherAnnonceUseCase
{
    private readonly IAnnonceGateway _annonceGateway;

    public RechercherAnnonceUseCase(IAnnonceGateway annonceGateway)
    {
        _annonceGateway = annonceGateway ?? throw new ArgumentNullException(nameof(annonceGateway));
    }

    public IEnumerable<Annonce> Execute(string? termeRecherche = null, int? categorieId = null)
    {
        var annonces = categorieId.HasValue
            ? _annonceGateway.GetAnnoncesByCategorieId(categorieId.Value)
            : _annonceGateway.GetAllAnnonces();

        if (string.IsNullOrWhiteSpace(termeRecherche))
        {
            return annonces;
        }

        return annonces.Where(a =>
            a.Nom.Contains(termeRecherche, StringComparison.OrdinalIgnoreCase) ||
            (!string.IsNullOrWhiteSpace(a.Description) &&
             a.Description.Contains(termeRecherche, StringComparison.OrdinalIgnoreCase)));
    }
}
