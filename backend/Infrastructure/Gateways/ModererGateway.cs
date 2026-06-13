using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class ModererGateway : IModererGateway
{
    private readonly IModererRepository _modererRepository;

    public ModererGateway(IModererRepository modererRepository)
    {
        _modererRepository = modererRepository ?? throw new ArgumentNullException(nameof(modererRepository));
    }

    public IEnumerable<Moderer> GetAllModerations()
    {
        return _modererRepository.GetAllModerations();
    }

    public Moderer? GetModerationById(int moderationId)
    {
        return _modererRepository.GetModerationById(moderationId);
    }

    public IEnumerable<Moderer> GetModerationsByAnnonceId(int annonceId)
    {
        return _modererRepository.GetModerationsByAnnonceId(annonceId);
    }

    public void AddModeration(Moderer moderation)
    {
        _modererRepository.AddModeration(moderation);
    }

    public void UpdateModeration(Moderer moderation)
    {
        _modererRepository.UpdateModeration(moderation);
    }

    public void DeleteModeration(int moderationId)
    {
        _modererRepository.DeleteModeration(moderationId);
    }
}
