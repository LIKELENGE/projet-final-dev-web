using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IStatutUtilisateurRepository
    {
        IEnumerable<StatutUtilisateur> GetAllStatutsUtilisateur();
        StatutUtilisateur? GetStatutUtilisateurById(int statutUtilisateurId);
        void AddStatutUtilisateur(StatutUtilisateur statutUtilisateur);
        void UpdateStatutUtilisateur(StatutUtilisateur statutUtilisateur);
        void DeleteStatutUtilisateur(int statutUtilisateurId);
    }
}