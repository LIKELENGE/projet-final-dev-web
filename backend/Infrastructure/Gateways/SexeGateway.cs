using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

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
        return _sexeRepository.GetAllSexes();
    }

    public Sexe? GetSexeById(int codeSexe)
    {
        return _sexeRepository.GetSexeById(codeSexe);
    }

    public void AddSexe(Sexe sexe)
    {
        _sexeRepository.AddSexe(sexe);
    }

    public void UpdateSexe(Sexe sexe)
    {
        _sexeRepository.UpdateSexe(sexe);
    }

    public void DeleteSexe(int codeSexe)
    {
        _sexeRepository.DeleteSexe(codeSexe);
    }
}
