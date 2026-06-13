using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

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
        return _statutUtilisateurRepository.GetAllStatutsUtilisateur();
    }

    public StatutUtilisateur? GetStatutUtilisateurById(int statutUtilisateurId)
    {
        return _statutUtilisateurRepository.GetStatutUtilisateurById(statutUtilisateurId);
    }

    public void AddStatutUtilisateur(StatutUtilisateur statutUtilisateur)
    {
        _statutUtilisateurRepository.AddStatutUtilisateur(statutUtilisateur);
    }

    public void UpdateStatutUtilisateur(StatutUtilisateur statutUtilisateur)
    {
        _statutUtilisateurRepository.UpdateStatutUtilisateur(statutUtilisateur);
    }

    public void DeleteStatutUtilisateur(int statutUtilisateurId)
    {
        _statutUtilisateurRepository.DeleteStatutUtilisateur(statutUtilisateurId);
    }
}
