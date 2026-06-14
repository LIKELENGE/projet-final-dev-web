using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _serviceRepository.GetAllServices().Select(service => service.ToCore());
    }

    public Service? GetServiceByAnnonceId(int annonceId)
    {
        var service = _serviceRepository.GetServiceByAnnonceId(annonceId);

        return service?.ToCore();
    }

    public void AddService(Service service)
    {
        _serviceRepository.AddService(service.ToInfrastructure());
    }

    public void DeleteService(int annonceId)
    {
        _serviceRepository.DeleteService(annonceId);
    }
}
