using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class InteretUtilisateurGateway : IInteretUtilisateurGateway
{
    private readonly IInteretUtilisateurRepository _interetUtilisateurRepository;

    public InteretUtilisateurGateway(IInteretUtilisateurRepository interetUtilisateurRepository)
    {
        _interetUtilisateurRepository = interetUtilisateurRepository ?? throw new ArgumentNullException(nameof(interetUtilisateurRepository));
    }

    public IEnumerable<InteretUtilisateur> GetAllInteretsUtilisateur()
    {
        return _interetUtilisateurRepository.GetAllInteretsUtilisateur();
    }

    public InteretUtilisateur? GetInteretUtilisateurById(int interetUtilisateurId)
    {
        return _interetUtilisateurRepository.GetInteretUtilisateurById(interetUtilisateurId);
    }

    public IEnumerable<InteretUtilisateur> GetInteretsByUtilisateurId(int utilisateurId)
    {
        return _interetUtilisateurRepository.GetInteretsByUtilisateurId(utilisateurId);
    }

    public void AddInteretUtilisateur(InteretUtilisateur interetUtilisateur)
    {
        _interetUtilisateurRepository.AddInteretUtilisateur(interetUtilisateur);
    }

    public void DeleteInteretUtilisateur(int interetUtilisateurId)
    {
        _interetUtilisateurRepository.DeleteInteretUtilisateur(interetUtilisateurId);
    }
}
