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
    public class EtatProduitRepository : IEtatProduitRepository
    {
        private readonly string _connectionString;

        public EtatProduitRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<EtatProduit> GetAllEtatsProduit()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM EtatProduit";
            return connection.Query<EtatProduit>(sql);
        }

        public EtatProduit? GetEtatProduitById(int etatProduitId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM EtatProduit WHERE Id_EtatProduit = @IdEtatProduit";
            return connection.QuerySingleOrDefault<EtatProduit>(sql, new { IdEtatProduit = etatProduitId });
        }

        public void AddEtatProduit(EtatProduit etatProduit)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO EtatProduit (NomEtat) VALUES (@NomEtat); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, etatProduit);
            etatProduit.IdEtatProduit = (int)id;
        }

        public void UpdateEtatProduit(EtatProduit etatProduit)
        {
            using var connection = GetConnection();
            const string sql = "UPDATE EtatProduit SET NomEtat = @NomEtat WHERE Id_EtatProduit = @IdEtatProduit";
            connection.Execute(sql, etatProduit);
        }

        public void DeleteEtatProduit(int etatProduitId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM EtatProduit WHERE Id_EtatProduit = @IdEtatProduit";
            connection.Execute(sql, new { IdEtatProduit = etatProduitId });
        }
    }
}
