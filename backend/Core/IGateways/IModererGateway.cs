using Core.Models;

namespace Core.IGateways;

public interface IModererGateway
{
    IEnumerable<Moderer> GetAllModerations();
    Moderer? GetModerationById(int moderationId);
    IEnumerable<Moderer> GetModerationsByAnnonceId(int annonceId);
    void AddModeration(Moderer moderation);
    void UpdateModeration(Moderer moderation);
    void DeleteModeration(int moderationId);
}
