using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class DetailStatutUtilisateurGateway : IDetailStatutUtilisateurGateway
{
    private readonly IDetailStatutUtilisateurRepository _detailStatutUtilisateurRepository;

    public DetailStatutUtilisateurGateway(IDetailStatutUtilisateurRepository detailStatutUtilisateurRepository)
    {
        _detailStatutUtilisateurRepository = detailStatutUtilisateurRepository ?? throw new ArgumentNullException(nameof(detailStatutUtilisateurRepository));
    }

    public IEnumerable<DetailStatutUtilisateur> GetAllDetailsStatutUtilisateur()
    {
        return _detailStatutUtilisateurRepository.GetAllDetailsStatutUtilisateur();
    }

    public DetailStatutUtilisateur? GetDetailStatutUtilisateurById(int detailStatutUtilisateurId)
    {
        return _detailStatutUtilisateurRepository.GetDetailStatutUtilisateurById(detailStatutUtilisateurId);
    }

    public IEnumerable<DetailStatutUtilisateur> GetDetailsByUtilisateurId(int utilisateurId)
    {
        return _detailStatutUtilisateurRepository.GetDetailsByUtilisateurId(utilisateurId);
    }

    public void AddDetailStatutUtilisateur(DetailStatutUtilisateur detailStatutUtilisateur)
    {
        _detailStatutUtilisateurRepository.AddDetailStatutUtilisateur(detailStatutUtilisateur);
    }

    public void UpdateDetailStatutUtilisateur(DetailStatutUtilisateur detailStatutUtilisateur)
    {
        _detailStatutUtilisateurRepository.UpdateDetailStatutUtilisateur(detailStatutUtilisateur);
    }

    public void DeleteDetailStatutUtilisateur(int detailStatutUtilisateurId)
    {
        _detailStatutUtilisateurRepository.DeleteDetailStatutUtilisateur(detailStatutUtilisateurId);
    }
}
