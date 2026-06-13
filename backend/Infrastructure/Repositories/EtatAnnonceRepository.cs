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
    public class EtatAnnonceRepository : IEtatAnnonceRepository
    {
        private readonly string _connectionString;

        public EtatAnnonceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<EtatAnnonce> GetAllEtatsAnnonce()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM EtatAnnonce";
            return connection.Query<EtatAnnonce>(sql);
        }

        public EtatAnnonce? GetEtatAnnonceById(int etatId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM EtatAnnonce WHERE Id_Etat = @IdEtat";
            return connection.QuerySingleOrDefault<EtatAnnonce>(sql, new { IdEtat = etatId });
        }

        public void AddEtatAnnonce(EtatAnnonce etatAnnonce)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO EtatAnnonce (NomEtat) VALUES (@NomEtat); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, etatAnnonce);
            etatAnnonce.IdEtat = (int)id;
        }

        public void UpdateEtatAnnonce(EtatAnnonce etatAnnonce)
        {
            using var connection = GetConnection();
            const string sql = "UPDATE EtatAnnonce SET NomEtat = @NomEtat WHERE Id_Etat = @IdEtat";
            connection.Execute(sql, etatAnnonce);
        }

        public void DeleteEtatAnnonce(int etatId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM EtatAnnonce WHERE Id_Etat = @IdEtat";
            connection.Execute(sql, new { IdEtat = etatId });
        }
    }
}
