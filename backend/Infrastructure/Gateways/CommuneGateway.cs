using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class CommuneGateway : ICommuneGateway
{
    private readonly ICommuneRepository _communeRepository;

    public CommuneGateway(ICommuneRepository communeRepository)
    {
        _communeRepository = communeRepository ?? throw new ArgumentNullException(nameof(communeRepository));
    }

    public IEnumerable<Commune> GetAllCommunes()
    {
        return _communeRepository.GetAllCommunes().Select(commune => commune.ToCore());
    }

    public Commune? GetCommuneById(int communeId)
    {
        var commune = _communeRepository.GetCommuneById(communeId);

        return commune?.ToCore();
    }

    public void AddCommune(Commune commune)
    {
        var communeDb = commune.ToInfrastructure();
        _communeRepository.AddCommune(communeDb);
        commune.Id = communeDb.IdCommune;
    }

    public void UpdateCommune(Commune commune)
    {
        _communeRepository.UpdateCommune(commune.ToInfrastructure());
    }

    public void DeleteCommune(int communeId)
    {
        _communeRepository.DeleteCommune(communeId);
    }
}
