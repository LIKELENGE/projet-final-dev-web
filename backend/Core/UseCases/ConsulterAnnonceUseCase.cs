using Core.IGateways;
using Core.Models;
using Core.Services;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ConsulterAnnonceUseCase : IConsulterAnnonceUseCase
{
    private readonly IAnnonceGateway _annonceGateway;
    private readonly IAnnonceLibelleService _annonceLibelleService;

    public ConsulterAnnonceUseCase(IAnnonceGateway annonceGateway, IAnnonceLibelleService annonceLibelleService)
    {
        _annonceGateway = annonceGateway ?? throw new ArgumentNullException(nameof(annonceGateway));
        _annonceLibelleService = annonceLibelleService ?? throw new ArgumentNullException(nameof(annonceLibelleService));
    }

    public Annonce? Execute(int annonceId)
    {
        if (annonceId <= 0)
        {
            throw new ArgumentException("L'identifiant de l'annonce est invalide.", nameof(annonceId));
        }

        var annonce = _annonceGateway.GetAnnonceById(annonceId);

        return annonce == null ? null : _annonceLibelleService.Completer(annonce);
    }
}
