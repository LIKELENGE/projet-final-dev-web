using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ConsulterAnnonceUseCase : IConsulterAnnonceUseCase
{
    private readonly IAnnonceGateway _annonceGateway;

    public ConsulterAnnonceUseCase(IAnnonceGateway annonceGateway)
    {
        _annonceGateway = annonceGateway ?? throw new ArgumentNullException(nameof(annonceGateway));
    }

    public Annonce? Execute(int annonceId)
    {
        if (annonceId <= 0)
        {
            throw new ArgumentException("L'identifiant de l'annonce est invalide.", nameof(annonceId));
        }

        return _annonceGateway.GetAnnonceById(annonceId);
    }
}
