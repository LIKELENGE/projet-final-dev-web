using Core.Models;

namespace Core.IGateways;

public interface IAdminGateway
{
    IEnumerable<Admin> GetAllAdmins();
    Admin? GetAdminById(int compte);
    void AddAdmin(Admin admin);
    void UpdateAdmin(Admin admin);
    void DeleteAdmin(int compte);
}
