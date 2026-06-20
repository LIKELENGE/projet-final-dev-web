using Core.IGateways;
using Core.Models;
using Core.Security;
using Core.UseCases.Abstractions;

namespace Core.UseCases;

public class ConnecterUtilisateurUseCase : IConnecterUtilisateurUseCase
{
    private readonly IUtilisateurGateway _utilisateurGateway;

    public ConnecterUtilisateurUseCase(IUtilisateurGateway utilisateurGateway)
    {
        _utilisateurGateway = utilisateurGateway ?? throw new ArgumentNullException(nameof(utilisateurGateway));
    }

    public Utilisateur Execute(string mail, string motDePasse)
    {
        if (string.IsNullOrWhiteSpace(mail))
        {
            throw new ArgumentException("L'adresse mail est obligatoire.", nameof(mail));
        }

        if (string.IsNullOrWhiteSpace(motDePasse))
        {
            throw new ArgumentException("Le mot de passe est obligatoire.", nameof(motDePasse));
        }

        var utilisateur = _utilisateurGateway.GetUtilisateurByMail(mail);

        if (utilisateur == null)
        {
            throw new InvalidOperationException("Identifiants invalides.");
        }

        var motDePasseHash = PasswordHasher.Hash(motDePasse);

        if (utilisateur.MotDePasseHash != motDePasseHash)
        {
            throw new InvalidOperationException("Identifiants invalides.");
        }

        return utilisateur;
    }
}
