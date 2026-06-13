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
    public class InteretUtilisateurRepository : IInteretUtilisateurRepository
    {
        private readonly string _connectionString;

        public InteretUtilisateurRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<InteretUtilisateur> GetAllInteretsUtilisateur()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM InteretUtilisateur";
            return connection.Query<InteretUtilisateur>(sql);
        }

        public InteretUtilisateur? GetInteretUtilisateurById(int interetUtilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM InteretUtilisateur WHERE Id_InteretUtilisateur = @IdInteretUtilisateur";
            return connection.QuerySingleOrDefault<InteretUtilisateur>(sql, new { IdInteretUtilisateur = interetUtilisateurId });
        }

        public IEnumerable<InteretUtilisateur> GetInteretsByUtilisateurId(int utilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM InteretUtilisateur WHERE Id_Utilisateur = @IdUtilisateur";
            return connection.Query<InteretUtilisateur>(sql, new { IdUtilisateur = utilisateurId });
        }

        public void AddInteretUtilisateur(InteretUtilisateur interetUtilisateur)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO InteretUtilisateur (Id_Utilisateur, Id_Categorie, Id_Commune, DateConsultation)
                                 VALUES (@IdUtilisateur, @IdCategorie, @IdCommune, @DateConsultation);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, interetUtilisateur);
            interetUtilisateur.IdInteretUtilisateur = (int)id;
        }

        public void DeleteInteretUtilisateur(int interetUtilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM InteretUtilisateur WHERE Id_InteretUtilisateur = @IdInteretUtilisateur";
            connection.Execute(sql, new { IdInteretUtilisateur = interetUtilisateurId });
        }
    }
}
