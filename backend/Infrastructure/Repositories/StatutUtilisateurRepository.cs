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
    public class StatutUtilisateurRepository : IStatutUtilisateurRepository
    {
        private readonly string _connectionString;

        public StatutUtilisateurRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<StatutUtilisateur> GetAllStatutsUtilisateur()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM StatutUtilisateur";
            return connection.Query<StatutUtilisateur>(sql);
        }

        public StatutUtilisateur? GetStatutUtilisateurById(int statutUtilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM StatutUtilisateur WHERE Id_StatutUtilisateur = @IdStatutUtilisateur";
            return connection.QuerySingleOrDefault<StatutUtilisateur>(sql, new { IdStatutUtilisateur = statutUtilisateurId });
        }

        public void AddStatutUtilisateur(StatutUtilisateur statutUtilisateur)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO StatutUtilisateur (NomStatutUtilisateur) VALUES (@NomStatutUtilisateur); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, statutUtilisateur);
            statutUtilisateur.IdStatutUtilisateur = (int)id;
        }

        public void UpdateStatutUtilisateur(StatutUtilisateur statutUtilisateur)
        {
            using var connection = GetConnection();
            const string sql = "UPDATE StatutUtilisateur SET NomStatutUtilisateur = @NomStatutUtilisateur WHERE Id_StatutUtilisateur = @IdStatutUtilisateur";
            connection.Execute(sql, statutUtilisateur);
        }

        public void DeleteStatutUtilisateur(int statutUtilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM StatutUtilisateur WHERE Id_StatutUtilisateur = @IdStatutUtilisateur";
            connection.Execute(sql, new { IdStatutUtilisateur = statutUtilisateurId });
        }
    }
}
