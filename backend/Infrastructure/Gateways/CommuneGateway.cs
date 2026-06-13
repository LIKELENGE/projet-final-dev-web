using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

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
        return _communeRepository.GetAllCommunes();
    }

    public Commune? GetCommuneById(int communeId)
    {
        return _communeRepository.GetCommuneById(communeId);
    }

    public void AddCommune(Commune commune)
    {
        _communeRepository.AddCommune(commune);
    }

    public void UpdateCommune(Commune commune)
    {
        _communeRepository.UpdateCommune(commune);
    }

    public void DeleteCommune(int communeId)
    {
        _communeRepository.DeleteCommune(communeId);
    }
}
