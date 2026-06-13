using Core.Models;

namespace Core.IGateways;

public interface ICommuneGateway
{
    IEnumerable<Commune> GetAllCommunes();
    Commune? GetCommuneById(int communeId);
    void AddCommune(Commune commune);
    void UpdateCommune(Commune commune);
    void DeleteCommune(int communeId);
}
