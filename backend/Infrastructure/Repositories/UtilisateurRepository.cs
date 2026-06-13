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
    public class UtilisateurRepository : IUtilisateurRepository
    {
        private readonly string _connectionString;

        public UtilisateurRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Utilisateur> GetAllUtilisateurs()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Utilisateur";
            return connection.Query<Utilisateur>(sql);
        }

        public Utilisateur? GetUtilisateurById(int utilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Utilisateur WHERE Id_Utilisateur = @IdUtilisateur";
            return connection.QuerySingleOrDefault<Utilisateur>(sql, new { IdUtilisateur = utilisateurId });
        }

        public Utilisateur? GetUtilisateurByMail(string mail)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Utilisateur WHERE Mail = @Mail";
            return connection.QuerySingleOrDefault<Utilisateur>(sql, new { Mail = mail });
        }

        public void AddUtilisateur(Utilisateur utilisateur)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO Utilisateur (Nom, Prenom, Mail, Tel, PhotoProfil, Mp, DateInscription, DateNaiss, CodeSexe, IdCommune)
                                 VALUES (@Nom, @Prenom, @Mail, @Tel, @PhotoProfil, @Mp, @DateInscription, @DateNaiss, @CodeSexe, @IdCommune);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, utilisateur);
            // if IdUtilisateur is int in model, cast carefully
            utilisateur.IdUtilisateur = (int)id;
        }

        public void UpdateUtilisateur(Utilisateur utilisateur)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE Utilisateur SET
                                    Nom = @Nom,
                                    Prenom = @Prenom,
                                    Mail = @Mail,
                                    Tel = @Tel,
                                    PhotoProfil = @PhotoProfil,
                                    Mp = @Mp,
                                    DateInscription = @DateInscription,
                                    DateNaiss = @DateNaiss,
                                    CodeSexe = @CodeSexe,
                                    IdCommune = @IdCommune
                                 WHERE Id_Utilisateur = @IdUtilisateur";

            connection.Execute(sql, utilisateur);
        }

        public void DeleteUtilisateur(int utilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM Utilisateur WHERE Id_Utilisateur = @IdUtilisateur";
            connection.Execute(sql, new { IdUtilisateur = utilisateurId });
        }
    }
}
