using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IDimensionRepository
    {
        IEnumerable<Dimension> GetAllDimensions();
        Dimension? GetDimensionById(int dimensionId);
        Dimension? GetDimensionByAnnonceId(int annonceId);
        void AddDimension(Dimension dimension);
        void UpdateDimension(Dimension dimension);
        void DeleteDimension(int dimensionId);
    }
}