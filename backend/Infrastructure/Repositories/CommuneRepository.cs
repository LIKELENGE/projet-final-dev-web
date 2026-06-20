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
    public class CommuneRepository : ICommuneRepository
    {
        private readonly string _connectionString;

        public CommuneRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Commune> GetAllCommunes()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM commune";
            return connection.Query<Commune>(sql);
        }

        public Commune? GetCommuneById(int communeId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM commune WHERE id_commune = @IdCommune";
            return connection.QuerySingleOrDefault<Commune>(sql, new { IdCommune = communeId });
        }

        public void AddCommune(Commune commune)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO commune (nom_commune, code_postal) VALUES (@NomCommune, @CodePostal); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, new { commune.NomCommune, commune.CodePostal });
            commune.IdCommune = (int)id;
        }

        public void UpdateCommune(Commune commune)
        {
            using var connection = GetConnection();
            const string sql = "UPDATE commune SET nom_commune = @NomCommune, code_postal = @CodePostal WHERE id_commune = @IdCommune";
            connection.Execute(sql, new { commune.NomCommune, commune.CodePostal, commune.IdCommune });
        }

        public void DeleteCommune(int communeId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM commune WHERE id_commune = @IdCommune";
            connection.Execute(sql, new { IdCommune = communeId });
        }
    }
}
