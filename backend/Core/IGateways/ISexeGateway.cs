using Core.Models;

namespace Core.IGateways;

public interface ISexeGateway
{
    IEnumerable<Sexe> GetAllSexes();
    Sexe? GetSexeById(int codeSexe);
    void AddSexe(Sexe sexe);
    void UpdateSexe(Sexe sexe);
    void DeleteSexe(int codeSexe);
}
