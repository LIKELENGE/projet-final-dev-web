using Core.IGateways;
using Core.Models;
using Core.Services;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ConsulterMesAnnoncesUseCase : IConsulterMesAnnoncesUseCase
{
    private readonly IAnnonceGateway _annonceGateway;
    private readonly IUtilisateurGateway _utilisateurGateway;
    private readonly IAnnonceLibelleService _annonceLibelleService;

    public ConsulterMesAnnoncesUseCase(
        IAnnonceGateway annonceGateway,
        IUtilisateurGateway utilisateurGateway,
        IAnnonceLibelleService annonceLibelleService)
    {
        _annonceGateway = annonceGateway ?? throw new ArgumentNullException(nameof(annonceGateway));
        _utilisateurGateway = utilisateurGateway ?? throw new ArgumentNullException(nameof(utilisateurGateway));
        _annonceLibelleService = annonceLibelleService ?? throw new ArgumentNullException(nameof(annonceLibelleService));
    }

    public IEnumerable<Annonce> Execute(int utilisateurId)
    {
        if (utilisateurId <= 0)
        {
            throw new ArgumentException("L'identifiant de l'utilisateur est invalide.", nameof(utilisateurId));
        }

        if (_utilisateurGateway.GetUtilisateurById(utilisateurId) == null)
        {
            throw new InvalidOperationException("L'utilisateur est introuvable.");
        }

        return _annonceLibelleService.Completer(_annonceGateway.GetAnnoncesByUtilisateurId(utilisateurId));
    }
}
