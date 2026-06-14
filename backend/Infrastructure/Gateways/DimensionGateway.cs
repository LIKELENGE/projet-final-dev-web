using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

namespace Infrastructure.Gateways;

public class DimensionGateway : IDimensionGateway
{
    private readonly IDimensionRepository _dimensionRepository;

    public DimensionGateway(IDimensionRepository dimensionRepository)
    {
        _dimensionRepository = dimensionRepository ?? throw new ArgumentNullException(nameof(dimensionRepository));
    }

    public IEnumerable<Dimension> GetAllDimensions()
    {
        return _dimensionRepository.GetAllDimensions().Select(dimension => dimension.ToCore());
    }

    public Dimension? GetDimensionById(int dimensionId)
    {
        var dimension = _dimensionRepository.GetDimensionById(dimensionId);

        return dimension?.ToCore();
    }

    public Dimension? GetDimensionByAnnonceId(int annonceId)
    {
        var dimension = _dimensionRepository.GetDimensionByAnnonceId(annonceId);

        return dimension?.ToCore();
    }

    public void AddDimension(Dimension dimension)
    {
        var dimensionDb = dimension.ToInfrastructure();
        _dimensionRepository.AddDimension(dimensionDb);
        dimension.Id = dimensionDb.IdDimension;
    }

    public void UpdateDimension(Dimension dimension)
    {
        _dimensionRepository.UpdateDimension(dimension.ToInfrastructure());
    }

    public void DeleteDimension(int dimensionId)
    {
        _dimensionRepository.DeleteDimension(dimensionId);
    }
}
