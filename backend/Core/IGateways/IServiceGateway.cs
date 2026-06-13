using Core.Models;

namespace Core.IGateways;

public interface IServiceGateway
{
    IEnumerable<Service> GetAllServices();
    Service? GetServiceByAnnonceId(int annonceId);
    void AddService(Service service);
    void DeleteService(int annonceId);
}
