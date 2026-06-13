using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IUtilisateurRepository
    {
        IEnumerable<Utilisateur> GetAllUtilisateurs();
        Utilisateur? GetUtilisateurById(int utilisateurId);
        Utilisateur? GetUtilisateurByMail(string mail);
        void AddUtilisateur(Utilisateur utilisateur);
        void UpdateUtilisateur(Utilisateur utilisateur);
        void DeleteUtilisateur(int utilisateurId);
    }
}