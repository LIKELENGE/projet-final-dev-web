using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IModererRepository
    {
        IEnumerable<Moderer> GetAllModerations();
        Moderer? GetModerationById(int moderationId);
        IEnumerable<Moderer> GetModerationsByAnnonceId(int annonceId);
        void AddModeration(Moderer moderation);
        void UpdateModeration(Moderer moderation);
        void DeleteModeration(int moderationId);
    }
}