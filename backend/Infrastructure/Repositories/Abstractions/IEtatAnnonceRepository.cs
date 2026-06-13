using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IEtatAnnonceRepository
    {
        IEnumerable<EtatAnnonce> GetAllEtatsAnnonce();
        EtatAnnonce? GetEtatAnnonceById(int etatId);
        void AddEtatAnnonce(EtatAnnonce etatAnnonce);
        void UpdateEtatAnnonce(EtatAnnonce etatAnnonce);
        void DeleteEtatAnnonce(int etatId);
    }
}