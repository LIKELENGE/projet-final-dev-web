using Core.IGateways;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;

namespace Infrastructure.Gateways;

public class AdminGateway : IAdminGateway
{
    private readonly IAdminRepository _adminRepository;

    public AdminGateway(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository ?? throw new ArgumentNullException(nameof(adminRepository));
    }

    public IEnumerable<Admin> GetAllAdmins()
    {
        return _adminRepository.GetAllAdmins();
    }

    public Admin? GetAdminById(int compte)
    {
        return _adminRepository.GetAdminById(compte);
    }

    public void AddAdmin(Admin admin)
    {
        _adminRepository.AddAdmin(admin);
    }

    public void UpdateAdmin(Admin admin)
    {
        _adminRepository.UpdateAdmin(admin);
    }

    public void DeleteAdmin(int compte)
    {
        _adminRepository.DeleteAdmin(compte);
    }
}
