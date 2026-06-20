using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class GererPhotosAnnonceUseCase : IGererPhotosAnnonceUseCase
{
    private readonly IPhotoAnnonceGateway _photoAnnonceGateway;
    private readonly IAnnonceGateway _annonceGateway;

    public GererPhotosAnnonceUseCase(IPhotoAnnonceGateway photoAnnonceGateway, IAnnonceGateway annonceGateway)
    {
        _photoAnnonceGateway = photoAnnonceGateway ?? throw new ArgumentNullException(nameof(photoAnnonceGateway));
        _annonceGateway = annonceGateway ?? throw new ArgumentNullException(nameof(annonceGateway));
    }

    public IEnumerable<PhotoAnnonce> ListerParAnnonce(int annonceId)
    {
        if (annonceId <= 0)
        {
            throw new ArgumentException("L'identifiant de l'annonce est invalide.", nameof(annonceId));
        }

        return _photoAnnonceGateway.GetPhotosByAnnonceId(annonceId);
    }

    public void Ajouter(PhotoAnnonce photoAnnonce, int utilisateurId)
    {
        ArgumentNullException.ThrowIfNull(photoAnnonce);

        if (photoAnnonce.Annonce == null || photoAnnonce.Annonce.Id <= 0)
        {
            throw new ArgumentException("La photo doit etre liee a une annonce.", nameof(photoAnnonce));
        }

        if (string.IsNullOrWhiteSpace(photoAnnonce.Lien))
        {
            throw new ArgumentException("Le lien de l'image est obligatoire.", nameof(photoAnnonce));
        }

        var annonce = _annonceGateway.GetAnnonceById(photoAnnonce.Annonce.Id);

        if (annonce == null)
        {
            throw new InvalidOperationException("L'annonce est introuvable.");
        }

        if (annonce.Utilisateur == null || annonce.Utilisateur.Id != utilisateurId)
        {
            throw new UnauthorizedAccessException("Seul l'auteur de l'annonce peut ajouter des images.");
        }

        _photoAnnonceGateway.AddPhotoAnnonce(photoAnnonce);
    }
}
