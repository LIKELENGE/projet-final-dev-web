using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure.Repositories.Abstractions
{
    public interface IAdminRepository
    {
        IEnumerable<Admin> GetAllAdmins();
        Admin? GetAdminById(int compte);
        void AddAdmin(Admin admin);
        void UpdateAdmin(Admin admin);
        void DeleteAdmin(int compte);
    }
}