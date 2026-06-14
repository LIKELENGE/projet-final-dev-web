using Core.IGateways;
using Core.Models;
using Infrastructure.Repositories.Abstractions;
using Infrastructure.Utils;

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
        return _adminRepository.GetAllAdmins().Select(admin => admin.ToCore());
    }

    public Admin? GetAdminById(int compte)
    {
        var admin = _adminRepository.GetAdminById(compte);

        return admin?.ToCore();
    }

    public void AddAdmin(Admin admin)
    {
        _adminRepository.AddAdmin(admin.ToInfrastructure());
    }

    public void UpdateAdmin(Admin admin)
    {
        _adminRepository.UpdateAdmin(admin.ToInfrastructure());
    }

    public void DeleteAdmin(int compte)
    {
        _adminRepository.DeleteAdmin(compte);
    }
}
