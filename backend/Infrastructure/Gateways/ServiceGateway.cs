using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class ServiceGateway : IServiceGateway
{
    private readonly IServiceRepository _serviceRepository;

    public ServiceGateway(IServiceRepository serviceRepository)
    {
        _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
    }

    public IEnumerable<Service> GetAllServices()
    {
        return _serviceRepository.GetAllServices();
    }

    public Service? GetServiceByAnnonceId(int annonceId)
    {
        return _serviceRepository.GetServiceByAnnonceId(annonceId);
    }

    public void AddService(Service service)
    {
        _serviceRepository.AddService(service);
    }

    public void DeleteService(int annonceId)
    {
        _serviceRepository.DeleteService(annonceId);
    }
}
