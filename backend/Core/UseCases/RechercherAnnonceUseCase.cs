using Core.IGateways;
using Core.Models;
using Core.Services;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class RechercherAnnonceUseCase : IRechercherAnnonceUseCase
{
    private readonly IAnnonceGateway _annonceGateway;
    private readonly IAnnonceLibelleService _annonceLibelleService;

    public RechercherAnnonceUseCase(IAnnonceGateway annonceGateway, IAnnonceLibelleService annonceLibelleService)
    {
        _annonceGateway = annonceGateway ?? throw new ArgumentNullException(nameof(annonceGateway));
        _annonceLibelleService = annonceLibelleService ?? throw new ArgumentNullException(nameof(annonceLibelleService));
    }

    public IEnumerable<Annonce> Execute(string? termeRecherche = null, int? categorieId = null)
    {
        var annonces = categorieId.HasValue
            ? _annonceGateway.GetAnnoncesByCategorieId(categorieId.Value)
            : _annonceGateway.GetAllAnnonces();

        if (string.IsNullOrWhiteSpace(termeRecherche))
        {
            return _annonceLibelleService.Completer(annonces);
        }

        return _annonceLibelleService.Completer(annonces.Where(a =>
            a.Nom.Contains(termeRecherche, StringComparison.OrdinalIgnoreCase) ||
            (!string.IsNullOrWhiteSpace(a.Description) &&
             a.Description.Contains(termeRecherche, StringComparison.OrdinalIgnoreCase))));
    }
}
