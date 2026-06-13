using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IServiceRepository
    {
        IEnumerable<Service> GetAllServices();
        Service? GetServiceByAnnonceId(int annonceId);
        void AddService(Service service);
        void DeleteService(int annonceId);
    }
}