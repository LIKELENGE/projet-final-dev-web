using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IInteretUtilisateurRepository
    {
        IEnumerable<InteretUtilisateur> GetAllInteretsUtilisateur();
        InteretUtilisateur? GetInteretUtilisateurById(int interetUtilisateurId);
        IEnumerable<InteretUtilisateur> GetInteretsByUtilisateurId(int utilisateurId);
        void AddInteretUtilisateur(InteretUtilisateur interetUtilisateur);
        void DeleteInteretUtilisateur(int interetUtilisateurId);
    }
}