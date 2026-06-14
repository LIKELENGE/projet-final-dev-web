using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _interetUtilisateurRepository.GetAllInteretsUtilisateur().Select(interet => interet.ToCore());
    }

    public InteretUtilisateur? GetInteretUtilisateurById(int interetUtilisateurId)
    {
        var interet = _interetUtilisateurRepository.GetInteretUtilisateurById(interetUtilisateurId);

        return interet?.ToCore();
    }

    public IEnumerable<InteretUtilisateur> GetInteretsByUtilisateurId(int utilisateurId)
    {
        return _interetUtilisateurRepository.GetInteretsByUtilisateurId(utilisateurId).Select(interet => interet.ToCore());
    }

    public void AddInteretUtilisateur(InteretUtilisateur interetUtilisateur)
    {
        var interetDb = interetUtilisateur.ToInfrastructure();
        _interetUtilisateurRepository.AddInteretUtilisateur(interetDb);
        interetUtilisateur.Id = interetDb.IdInteretUtilisateur;
    }

    public void DeleteInteretUtilisateur(int interetUtilisateurId)
    {
        _interetUtilisateurRepository.DeleteInteretUtilisateur(interetUtilisateurId);
    }
}
