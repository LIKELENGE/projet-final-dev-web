using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _detailStatutUtilisateurRepository.GetAllDetailsStatutUtilisateur().Select(detail => detail.ToCore());
    }

    public DetailStatutUtilisateur? GetDetailStatutUtilisateurById(int detailStatutUtilisateurId)
    {
        var detail = _detailStatutUtilisateurRepository.GetDetailStatutUtilisateurById(detailStatutUtilisateurId);

        return detail?.ToCore();
    }

    public IEnumerable<DetailStatutUtilisateur> GetDetailsByUtilisateurId(int utilisateurId)
    {
        return _detailStatutUtilisateurRepository.GetDetailsByUtilisateurId(utilisateurId).Select(detail => detail.ToCore());
    }

    public void AddDetailStatutUtilisateur(DetailStatutUtilisateur detailStatutUtilisateur)
    {
        var detailDb = detailStatutUtilisateur.ToInfrastructure();
        _detailStatutUtilisateurRepository.AddDetailStatutUtilisateur(detailDb);
        detailStatutUtilisateur.Id = detailDb.IdDetailStatutUtilisateur;
    }

    public void UpdateDetailStatutUtilisateur(DetailStatutUtilisateur detailStatutUtilisateur)
    {
        _detailStatutUtilisateurRepository.UpdateDetailStatutUtilisateur(detailStatutUtilisateur.ToInfrastructure());
    }

    public void DeleteDetailStatutUtilisateur(int detailStatutUtilisateurId)
    {
        _detailStatutUtilisateurRepository.DeleteDetailStatutUtilisateur(detailStatutUtilisateurId);
    }
}
