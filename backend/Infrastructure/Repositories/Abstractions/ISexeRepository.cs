using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface ISexeRepository
    {
        IEnumerable<Sexe> GetAllSexes();
        Sexe? GetSexeById(int codeSexe);
        void AddSexe(Sexe sexe);
        void UpdateSexe(Sexe sexe);
        void DeleteSexe(int codeSexe);
    }
}