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
            const string sql = "SELECT * FROM moderer";
            return connection.Query<Moderer>(sql);
        }

        public Moderer? GetModerationById(int moderationId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM moderer WHERE id_moderation = @IdModeration";
            return connection.QuerySingleOrDefault<Moderer>(sql, new { IdModeration = moderationId });
        }

        public IEnumerable<Moderer> GetModerationsByAnnonceId(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM moderer WHERE id_annonce = @IdAnnonce";
            return connection.Query<Moderer>(sql, new { IdAnnonce = annonceId });
        }

        public void AddModeration(Moderer moderation)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO moderer (compte, id_annonce, id_etat, date_statut, delai_statut, illimite)
                                 VALUES (@Compte, @IdAnnonce, @IdEtat, @DateStatut, @DelaiStatut, @Illimite);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, moderation);
            moderation.IdModeration = (int)id;
        }

        public void UpdateModeration(Moderer moderation)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE moderer SET
                                    compte = @Compte,
                                    id_annonce = @IdAnnonce,
                                    id_etat = @IdEtat,
                                    date_statut = @DateStatut,
                                    delai_statut = @DelaiStatut,
                                    illimite = @Illimite
                                 WHERE id_moderation = @IdModeration";

            connection.Execute(sql, moderation);
        }

        public void DeleteModeration(int moderationId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM moderer WHERE id_moderation = @IdModeration";
            connection.Execute(sql, new { IdModeration = moderationId });
        }
    }
}
