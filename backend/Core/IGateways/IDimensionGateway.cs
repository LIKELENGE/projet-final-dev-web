using Core.Models;

namespace Core.IGateways;

public interface IDimensionGateway
{
    IEnumerable<Dimension> GetAllDimensions();
    Dimension? GetDimensionById(int dimensionId);
    Dimension? GetDimensionByAnnonceId(int annonceId);
    void AddDimension(Dimension dimension);
    void UpdateDimension(Dimension dimension);
    void DeleteDimension(int dimensionId);
}
