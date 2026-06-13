using Core.Models;

namespace Core.IGateways;

public interface IUtilisateurGateway
{
    IEnumerable<Utilisateur> GetAllUtilisateurs();
    Utilisateur? GetUtilisateurById(int utilisateurId);
    Utilisateur? GetUtilisateurByMail(string mail);
    void AddUtilisateur(Utilisateur utilisateur);
    void UpdateUtilisateur(Utilisateur utilisateur);
    void DeleteUtilisateur(int utilisateurId);
}
