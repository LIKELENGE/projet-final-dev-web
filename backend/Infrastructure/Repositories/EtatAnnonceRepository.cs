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
            const string sql = "SELECT * FROM etat_annonce";
            return connection.Query<EtatAnnonce>(sql);
        }

        public EtatAnnonce? GetEtatAnnonceById(int etatId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM etat_annonce WHERE id_etat = @IdEtat";
            return connection.QuerySingleOrDefault<EtatAnnonce>(sql, new { IdEtat = etatId });
        }

        public void AddEtatAnnonce(EtatAnnonce etatAnnonce)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO etat_annonce (nom_etat) VALUES (@NomEtat); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, etatAnnonce);
            etatAnnonce.IdEtat = (int)id;
        }

        public void UpdateEtatAnnonce(EtatAnnonce etatAnnonce)
        {
            using var connection = GetConnection();
            const string sql = "UPDATE etat_annonce SET nom_etat = @NomEtat WHERE id_etat = @IdEtat";
            connection.Execute(sql, etatAnnonce);
        }

        public void DeleteEtatAnnonce(int etatId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM etat_annonce WHERE id_etat = @IdEtat";
            connection.Execute(sql, new { IdEtat = etatId });
        }
    }
}
