using Core.Models;

namespace Core.IGateways;

public interface IDetailStatutUtilisateurGateway
{
    IEnumerable<DetailStatutUtilisateur> GetAllDetailsStatutUtilisateur();
    DetailStatutUtilisateur? GetDetailStatutUtilisateurById(int detailStatutUtilisateurId);
    IEnumerable<DetailStatutUtilisateur> GetDetailsByUtilisateurId(int utilisateurId);
    void AddDetailStatutUtilisateur(DetailStatutUtilisateur detailStatutUtilisateur);
    void UpdateDetailStatutUtilisateur(DetailStatutUtilisateur detailStatutUtilisateur);
    void DeleteDetailStatutUtilisateur(int detailStatutUtilisateurId);
}
