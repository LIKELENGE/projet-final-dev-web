using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class UtilisateurGateway : IUtilisateurGateway
{
    private readonly IUtilisateurRepository _utilisateurRepository;

    public UtilisateurGateway(IUtilisateurRepository utilisateurRepository)
    {
        _utilisateurRepository = utilisateurRepository ?? throw new ArgumentNullException(nameof(utilisateurRepository));
    }

    public IEnumerable<Utilisateur> GetAllUtilisateurs()
    {
        return _utilisateurRepository.GetAllUtilisateurs().Select(utilisateur => utilisateur.ToCore());
    }

    public Utilisateur? GetUtilisateurById(int utilisateurId)
    {
        var utilisateur = _utilisateurRepository.GetUtilisateurById(utilisateurId);

        return utilisateur?.ToCore();
    }

    public Utilisateur? GetUtilisateurByMail(string mail)
    {
        var utilisateur = _utilisateurRepository.GetUtilisateurByMail(mail);

        return utilisateur?.ToCore();
    }

    public void AddUtilisateur(Utilisateur utilisateur)
    {
        var utilisateurDb = utilisateur.ToInfrastructure();
        _utilisateurRepository.AddUtilisateur(utilisateurDb);
        utilisateur.Id = utilisateurDb.IdUtilisateur;
    }

    public void UpdateUtilisateur(Utilisateur utilisateur)
    {
        _utilisateurRepository.UpdateUtilisateur(utilisateur.ToInfrastructure());
    }

    public void DeleteUtilisateur(int utilisateurId)
    {
        _utilisateurRepository.DeleteUtilisateur(utilisateurId);
    }
}
