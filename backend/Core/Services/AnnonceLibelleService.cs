using Core.IGateways;
using Core.Models;

namespace Core.Services;

public class AnnonceLibelleService : IAnnonceLibelleService
{
    private readonly ICategorieGateway _categorieGateway;
    private readonly ICommuneGateway _communeGateway;
    private readonly IUtilisateurGateway _utilisateurGateway;
    private readonly IPhotoAnnonceGateway _photoAnnonceGateway;

    public AnnonceLibelleService(
        ICategorieGateway categorieGateway,
        ICommuneGateway communeGateway,
        IUtilisateurGateway utilisateurGateway,
        IPhotoAnnonceGateway photoAnnonceGateway)
    {
        _categorieGateway = categorieGateway ?? throw new ArgumentNullException(nameof(categorieGateway));
        _communeGateway = communeGateway ?? throw new ArgumentNullException(nameof(communeGateway));
        _utilisateurGateway = utilisateurGateway ?? throw new ArgumentNullException(nameof(utilisateurGateway));
        _photoAnnonceGateway = photoAnnonceGateway ?? throw new ArgumentNullException(nameof(photoAnnonceGateway));
    }

    public IEnumerable<Annonce> Completer(IEnumerable<Annonce> annonces)
    {
        return annonces.Select(Completer);
    }

    public Annonce Completer(Annonce annonce)
    {
        ArgumentNullException.ThrowIfNull(annonce);

        if (annonce.Categorie?.Id > 0)
        {
            annonce.Categorie = _categorieGateway.GetCategorieById(annonce.Categorie.Id) ?? annonce.Categorie;
        }

        if (annonce.Commune?.Id > 0)
        {
            annonce.Commune = _communeGateway.GetCommuneById(annonce.Commune.Id) ?? annonce.Commune;
        }

        if (annonce.Utilisateur?.Id > 0)
        {
            annonce.Utilisateur = _utilisateurGateway.GetUtilisateurById(annonce.Utilisateur.Id) ?? annonce.Utilisateur;
            annonce.Utilisateur.MotDePasseHash = string.Empty;
        }

        if (annonce.Id > 0)
        {
            annonce.Photos = _photoAnnonceGateway.GetPhotosByAnnonceId(annonce.Id).ToList();
        }

        return annonce;
    }
}
