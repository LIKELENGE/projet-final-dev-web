using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface ICommuneRepository
    {
        IEnumerable<Commune> GetAllCommunes();
        Commune? GetCommuneById(int communeId);
        void AddCommune(Commune commune);
        void UpdateCommune(Commune commune);
        void DeleteCommune(int communeId);
    }
}