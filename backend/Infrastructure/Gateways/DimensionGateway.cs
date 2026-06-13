using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

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
        return _dimensionRepository.GetAllDimensions();
    }

    public Dimension? GetDimensionById(int dimensionId)
    {
        return _dimensionRepository.GetDimensionById(dimensionId);
    }

    public Dimension? GetDimensionByAnnonceId(int annonceId)
    {
        return _dimensionRepository.GetDimensionByAnnonceId(annonceId);
    }

    public void AddDimension(Dimension dimension)
    {
        _dimensionRepository.AddDimension(dimension);
    }

    public void UpdateDimension(Dimension dimension)
    {
        _dimensionRepository.UpdateDimension(dimension);
    }

    public void DeleteDimension(int dimensionId)
    {
        _dimensionRepository.DeleteDimension(dimensionId);
    }
}
