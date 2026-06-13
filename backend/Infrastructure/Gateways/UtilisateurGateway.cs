using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

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
        return _utilisateurRepository.GetAllUtilisateurs();
    }

    public Utilisateur? GetUtilisateurById(int utilisateurId)
    {
        return _utilisateurRepository.GetUtilisateurById(utilisateurId);
    }

    public Utilisateur? GetUtilisateurByMail(string mail)
    {
        return _utilisateurRepository.GetUtilisateurByMail(mail);
    }

    public void AddUtilisateur(Utilisateur utilisateur)
    {
        _utilisateurRepository.AddUtilisateur(utilisateur);
    }

    public void UpdateUtilisateur(Utilisateur utilisateur)
    {
        _utilisateurRepository.UpdateUtilisateur(utilisateur);
    }

    public void DeleteUtilisateur(int utilisateurId)
    {
        _utilisateurRepository.DeleteUtilisateur(utilisateurId);
    }
}
