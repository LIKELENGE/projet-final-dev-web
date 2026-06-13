using Core.IGateways;
using Core.Models;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class InscrireUtilisateurUseCase : IInscrireUtilisateurUseCase
{
    private readonly IUtilisateurGateway _utilisateurGateway;

    public InscrireUtilisateurUseCase(IUtilisateurGateway utilisateurGateway)
    {
        _utilisateurGateway = utilisateurGateway ?? throw new ArgumentNullException(nameof(utilisateurGateway));
    }

    public void Execute(Utilisateur utilisateur)
    {
        ArgumentNullException.ThrowIfNull(utilisateur);

        if (string.IsNullOrWhiteSpace(utilisateur.Nom))
        {
            throw new ArgumentException("Le nom de l'utilisateur est obligatoire.", nameof(utilisateur));
        }

        if (string.IsNullOrWhiteSpace(utilisateur.Prenom))
        {
            throw new ArgumentException("Le prenom de l'utilisateur est obligatoire.", nameof(utilisateur));
        }

        if (string.IsNullOrWhiteSpace(utilisateur.Mail))
        {
            throw new ArgumentException("L'adresse mail est obligatoire.", nameof(utilisateur));
        }

        if (string.IsNullOrWhiteSpace(utilisateur.MotDePasseHash))
        {
            throw new ArgumentException("Le mot de passe est obligatoire.", nameof(utilisateur));
        }

        var utilisateurExistant = _utilisateurGateway.GetUtilisateurByMail(utilisateur.Mail);

        if (utilisateurExistant != null)
        {
            throw new InvalidOperationException("Un utilisateur existe deja avec cette adresse mail.");
        }

        if (utilisateur.DateInscription == default)
        {
            utilisateur.DateInscription = DateTime.UtcNow;
        }

        _utilisateurGateway.AddUtilisateur(utilisateur);
    }
}
