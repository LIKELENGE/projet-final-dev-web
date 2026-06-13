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
    public class ModererRepository : IModererRepository
    {
        private readonly string _connectionString;

        public ModererRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Moderer> GetAllModerations()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Moderer";
            return connection.Query<Moderer>(sql);
        }

        public Moderer? GetModerationById(int moderationId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Moderer WHERE Id_Moderation = @IdModeration";
            return connection.QuerySingleOrDefault<Moderer>(sql, new { IdModeration = moderationId });
        }

        public IEnumerable<Moderer> GetModerationsByAnnonceId(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Moderer WHERE Id_Annonce = @IdAnnonce";
            return connection.Query<Moderer>(sql, new { IdAnnonce = annonceId });
        }

        public void AddModeration(Moderer moderation)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO Moderer (Compte, Id_Annonce, Id_Etat, DateStatut, DelaiStatut, Illimite)
                                 VALUES (@Compte, @IdAnnonce, @IdEtat, @DateStatut, @DelaiStatut, @Illimite);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, moderation);
            moderation.IdModeration = (int)id;
        }

        public void UpdateModeration(Moderer moderation)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE Moderer SET
                                    Compte = @Compte,
                                    Id_Annonce = @IdAnnonce,
                                    Id_Etat = @IdEtat,
                                    DateStatut = @DateStatut,
                                    DelaiStatut = @DelaiStatut,
                                    Illimite = @Illimite
                                 WHERE Id_Moderation = @IdModeration";

            connection.Execute(sql, moderation);
        }

        public void DeleteModeration(int moderationId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM Moderer WHERE Id_Moderation = @IdModeration";
            connection.Execute(sql, new { IdModeration = moderationId });
        }
    }
}
