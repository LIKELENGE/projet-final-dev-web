using Core.Models;

namespace Core.IGateways;

public interface IInteretUtilisateurGateway
{
    IEnumerable<InteretUtilisateur> GetAllInteretsUtilisateur();
    InteretUtilisateur? GetInteretUtilisateurById(int interetUtilisateurId);
    IEnumerable<InteretUtilisateur> GetInteretsByUtilisateurId(int utilisateurId);
    void AddInteretUtilisateur(InteretUtilisateur interetUtilisateur);
    void DeleteInteretUtilisateur(int interetUtilisateurId);
}
