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
    public class DetailStatutUtilisateurRepository : IDetailStatutUtilisateurRepository
    {
        private readonly string _connectionString;

        public DetailStatutUtilisateurRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<DetailStatutUtilisateur> GetAllDetailsStatutUtilisateur()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM DetailStatutUtilisateur";
            return connection.Query<DetailStatutUtilisateur>(sql);
        }

        public DetailStatutUtilisateur? GetDetailStatutUtilisateurById(int detailStatutUtilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM DetailStatutUtilisateur WHERE Id_DetailStatutUtilisateur = @IdDetailStatutUtilisateur";
            return connection.QuerySingleOrDefault<DetailStatutUtilisateur>(sql, new { IdDetailStatutUtilisateur = detailStatutUtilisateurId });
        }

        public IEnumerable<DetailStatutUtilisateur> GetDetailsByUtilisateurId(int utilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM DetailStatutUtilisateur WHERE Id_Utilisateur = @IdUtilisateur";
            return connection.Query<DetailStatutUtilisateur>(sql, new { IdUtilisateur = utilisateurId });
        }

        public void AddDetailStatutUtilisateur(DetailStatutUtilisateur detailStatutUtilisateur)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO DetailStatutUtilisateur (Id_Utilisateur, Id_StatutUtilisateur, DateStatut, DelaiStatut, Illimite)
                                 VALUES (@IdUtilisateur, @IdStatutUtilisateur, @DateStatut, @DelaiStatut, @Illimite);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, detailStatutUtilisateur);
            detailStatutUtilisateur.IdDetailStatutUtilisateur = (int)id;
        }

        public void UpdateDetailStatutUtilisateur(DetailStatutUtilisateur detailStatutUtilisateur)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE DetailStatutUtilisateur SET
                                    Id_Utilisateur = @IdUtilisateur,
                                    Id_StatutUtilisateur = @IdStatutUtilisateur,
                                    DateStatut = @DateStatut,
                                    DelaiStatut = @DelaiStatut,
                                    Illimite = @Illimite
                                 WHERE Id_DetailStatutUtilisateur = @IdDetailStatutUtilisateur";

            connection.Execute(sql, detailStatutUtilisateur);
        }

        public void DeleteDetailStatutUtilisateur(int detailStatutUtilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM DetailStatutUtilisateur WHERE Id_DetailStatutUtilisateur = @IdDetailStatutUtilisateur";
            connection.Execute(sql, new { IdDetailStatutUtilisateur = detailStatutUtilisateurId });
        }
    }
}
