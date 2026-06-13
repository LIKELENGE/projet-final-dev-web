using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Infrastructure.Models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly string _connectionString;

        public AdminRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Admin> GetAllAdmins()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Admin";
            return connection.Query<Admin>(sql);
        }

        public Admin? GetAdminById(int compte)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Admin WHERE Compte = @Compte";
            return connection.QuerySingleOrDefault<Admin>(sql, new { Compte = compte });
        }

        public void AddAdmin(Admin admin)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO Admin (Nom, Prenom, Niveau, Mp)
                                 VALUES (@Nom, @Prenom, @Niveau, @Mp);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, new { admin.Nom, admin.Prenom, admin.Niveau, admin.Mp });
            admin.Compte = (int)id;
        }

        public void UpdateAdmin(Admin admin)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE Admin SET
                                    Nom = @Nom,
                                    Prenom = @Prenom,
                                    Niveau = @Niveau,
                                    Mp = @Mp
                                 WHERE Compte = @Compte";

            connection.Execute(sql, new { admin.Nom, admin.Prenom, admin.Niveau, admin.Mp, admin.Compte });
        }

        public void DeleteAdmin(int compte)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM Admin WHERE Compte = @Compte";
            connection.Execute(sql, new { Compte = compte });
        }
    }
}
