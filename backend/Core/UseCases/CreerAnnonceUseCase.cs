using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class CreerAnnonceUseCase : ICreerAnnonceUseCase
{
    private readonly IAnnonceGateway _annonceGateway;
    private readonly IUtilisateurGateway _utilisateurGateway;

    public CreerAnnonceUseCase(IAnnonceGateway annonceGateway, IUtilisateurGateway utilisateurGateway)
    {
        _annonceGateway = annonceGateway ?? throw new ArgumentNullException(nameof(annonceGateway));
        _utilisateurGateway = utilisateurGateway ?? throw new ArgumentNullException(nameof(utilisateurGateway));
    }

    public void Execute(Annonce annonce)
    {
        ArgumentNullException.ThrowIfNull(annonce);
        ValidateAnnonce(annonce);

        if (_utilisateurGateway.GetUtilisateurById(annonce.Utilisateur!.Id) == null)
        {
            throw new InvalidOperationException("L'utilisateur doit etre authentifie pour creer une annonce.");
        }

        if (annonce.DateAjout == default)
        {
            annonce.DateAjout = DateTime.UtcNow;
        }

        if (annonce.DerniereModification == default)
        {
            annonce.DerniereModification = annonce.DateAjout;
        }

        _annonceGateway.AddAnnonce(annonce);
    }

    private static void ValidateAnnonce(Annonce annonce)
    {
        if (annonce.Utilisateur == null || annonce.Utilisateur.Id <= 0)
        {
            throw new ArgumentException("L'annonce doit etre liee a un utilisateur.", nameof(annonce));
        }

        if (annonce.Categorie == null || annonce.Categorie.Id <= 0)
        {
            throw new ArgumentException("L'annonce doit etre liee a une categorie.", nameof(annonce));
        }

        if (string.IsNullOrWhiteSpace(annonce.Nom))
        {
            throw new ArgumentException("Le nom de l'annonce est obligatoire.", nameof(annonce));
        }

        if (annonce.Prix < 0)
        {
            throw new ArgumentException("Le prix de l'annonce ne peut pas etre negatif.", nameof(annonce));
        }
    }
}
