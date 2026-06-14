using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class StatutUtilisateurGateway : IStatutUtilisateurGateway
{
    private readonly IStatutUtilisateurRepository _statutUtilisateurRepository;

    public StatutUtilisateurGateway(IStatutUtilisateurRepository statutUtilisateurRepository)
    {
        _statutUtilisateurRepository = statutUtilisateurRepository ?? throw new ArgumentNullException(nameof(statutUtilisateurRepository));
    }

    public IEnumerable<StatutUtilisateur> GetAllStatutsUtilisateur()
    {
        return _statutUtilisateurRepository.GetAllStatutsUtilisateur().Select(statut => statut.ToCore());
    }

    public StatutUtilisateur? GetStatutUtilisateurById(int statutUtilisateurId)
    {
        var statut = _statutUtilisateurRepository.GetStatutUtilisateurById(statutUtilisateurId);

        return statut?.ToCore();
    }

    public void AddStatutUtilisateur(StatutUtilisateur statutUtilisateur)
    {
        var statutDb = statutUtilisateur.ToInfrastructure();
        _statutUtilisateurRepository.AddStatutUtilisateur(statutDb);
        statutUtilisateur.Id = statutDb.IdStatutUtilisateur;
    }

    public void UpdateStatutUtilisateur(StatutUtilisateur statutUtilisateur)
    {
        _statutUtilisateurRepository.UpdateStatutUtilisateur(statutUtilisateur.ToInfrastructure());
    }

    public void DeleteStatutUtilisateur(int statutUtilisateurId)
    {
        _statutUtilisateurRepository.DeleteStatutUtilisateur(statutUtilisateurId);
    }
}
