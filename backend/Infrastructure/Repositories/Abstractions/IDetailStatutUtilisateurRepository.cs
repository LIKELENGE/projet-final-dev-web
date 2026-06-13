using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IDetailStatutUtilisateurRepository
    {
        IEnumerable<DetailStatutUtilisateur> GetAllDetailsStatutUtilisateur();
        DetailStatutUtilisateur? GetDetailStatutUtilisateurById(int detailStatutUtilisateurId);
        IEnumerable<DetailStatutUtilisateur> GetDetailsByUtilisateurId(int utilisateurId);
        void AddDetailStatutUtilisateur(DetailStatutUtilisateur detailStatutUtilisateur);
        void UpdateDetailStatutUtilisateur(DetailStatutUtilisateur detailStatutUtilisateur);
        void DeleteDetailStatutUtilisateur(int detailStatutUtilisateurId);
    }
}