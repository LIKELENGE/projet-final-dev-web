using Core.Models;

namespace Core.IGateways;

public interface IStatutUtilisateurGateway
{
    IEnumerable<StatutUtilisateur> GetAllStatutsUtilisateur();
    StatutUtilisateur? GetStatutUtilisateurById(int statutUtilisateurId);
    void AddStatutUtilisateur(StatutUtilisateur statutUtilisateur);
    void UpdateStatutUtilisateur(StatutUtilisateur statutUtilisateur);
    void DeleteStatutUtilisateur(int statutUtilisateurId);
}
