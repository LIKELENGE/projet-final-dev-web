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
    public class ServiceRepository : IServiceRepository
    {
        private readonly string _connectionString;

        public ServiceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Service> GetAllServices()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Service";
            return connection.Query<Service>(sql);
        }

        public Service? GetServiceByAnnonceId(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Service WHERE Id_Annonce = @IdAnnonce";
            return connection.QuerySingleOrDefault<Service>(sql, new { IdAnnonce = annonceId });
        }

        public void AddService(Service service)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO Service (Id_Annonce) VALUES (@IdAnnonce); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, new { service.IdAnnonce });
            service.IdAnnonce = (int)id;
        }

        public void DeleteService(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM Service WHERE Id_Annonce = @IdAnnonce";
            connection.Execute(sql, new { IdAnnonce = annonceId });
        }
    }
}
