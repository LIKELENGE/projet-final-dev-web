using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _modererRepository.GetAllModerations().Select(moderation => moderation.ToCore());
    }

    public Moderer? GetModerationById(int moderationId)
    {
        var moderation = _modererRepository.GetModerationById(moderationId);

        return moderation?.ToCore();
    }

    public IEnumerable<Moderer> GetModerationsByAnnonceId(int annonceId)
    {
        return _modererRepository.GetModerationsByAnnonceId(annonceId).Select(moderation => moderation.ToCore());
    }

    public void AddModeration(Moderer moderation)
    {
        var moderationDb = moderation.ToInfrastructure();
        _modererRepository.AddModeration(moderationDb);
        moderation.Id = moderationDb.IdModeration;
    }

    public void UpdateModeration(Moderer moderation)
    {
        _modererRepository.UpdateModeration(moderation.ToInfrastructure());
    }

    public void DeleteModeration(int moderationId)
    {
        _modererRepository.DeleteModeration(moderationId);
    }
}
