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

    public void Execute(int annonceId, int utilisateurId)
    {
        if (annonceId <= 0)
        {
            throw new ArgumentException("L'identifiant de l'annonce est invalide.", nameof(annonceId));
        }

        if (utilisateurId <= 0)
        {
            throw new ArgumentException("L'identifiant de l'utilisateur est invalide.", nameof(utilisateurId));
        }

        var annonce = _annonceGateway.GetAnnonceById(annonceId);

        if (annonce == null)
        {
            throw new InvalidOperationException("L'annonce a supprimer est introuvable.");
        }

        if (annonce.Utilisateur == null || annonce.Utilisateur.Id != utilisateurId)
        {
            throw new UnauthorizedAccessException("Seul l'auteur de l'annonce peut la supprimer.");
        }

        _annonceGateway.DeleteAnnonce(annonceId);
    }
}
