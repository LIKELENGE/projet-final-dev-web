using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class SexeGateway : ISexeGateway
{
    private readonly ISexeRepository _sexeRepository;

    public SexeGateway(ISexeRepository sexeRepository)
    {
        _sexeRepository = sexeRepository ?? throw new ArgumentNullException(nameof(sexeRepository));
    }

    public IEnumerable<Sexe> GetAllSexes()
    {
        return _sexeRepository.GetAllSexes().Select(sexe => sexe.ToCore());
    }

    public Sexe? GetSexeById(int codeSexe)
    {
        var sexe = _sexeRepository.GetSexeById(codeSexe);

        return sexe?.ToCore();
    }

    public void AddSexe(Sexe sexe)
    {
        var sexeDb = sexe.ToInfrastructure();
        _sexeRepository.AddSexe(sexeDb);
        sexe.Code = sexeDb.CodeSexe;
    }

    public void UpdateSexe(Sexe sexe)
    {
        _sexeRepository.UpdateSexe(sexe.ToInfrastructure());
    }

    public void DeleteSexe(int codeSexe)
    {
        _sexeRepository.DeleteSexe(codeSexe);
    }
}
