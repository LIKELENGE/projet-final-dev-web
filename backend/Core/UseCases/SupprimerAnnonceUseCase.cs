using Core.IGateways;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class SupprimerAnnonceUseCase : ISupprimerAnnonceUseCase
{
    private readonly IAnnonceGateway _annonceGateway;

    public SupprimerAnnonceUseCase(IAnnonceGateway annonceGateway)
    {
        _annonceGateway = annonceGateway ?? throw new ArgumentNullException(nameof(annonceGateway));
    }

    public void Execute(int annonceId)
    {
        if (annonceId <= 0)
        {
            throw new ArgumentException("L'identifiant de l'annonce est invalide.", nameof(annonceId));
        }

        if (_annonceGateway.GetAnnonceById(annonceId) == null)
        {
            throw new InvalidOperationException("L'annonce a supprimer est introuvable.");
        }

        _annonceGateway.DeleteAnnonce(annonceId);
    }
}
