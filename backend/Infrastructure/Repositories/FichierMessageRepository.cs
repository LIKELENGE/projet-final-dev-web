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
    public class FichierMessageRepository : IFichierMessageRepository
    {
        private readonly string _connectionString;

        public FichierMessageRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<FichierMessage> GetAllFichiersMessage()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM FichierMessage";
            return connection.Query<FichierMessage>(sql);
        }

        public FichierMessage? GetFichierMessageById(int lienId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM FichierMessage WHERE Id_Lien = @IdLien";
            return connection.QuerySingleOrDefault<FichierMessage>(sql, new { IdLien = lienId });
        }

        public IEnumerable<FichierMessage> GetFichiersByMessageId(int messageId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM FichierMessage WHERE Id_Message = @IdMessage";
            return connection.Query<FichierMessage>(sql, new { IdMessage = messageId });
        }

        public void AddFichierMessage(FichierMessage fichierMessage)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO FichierMessage (Id_Message, Lien)
                                 VALUES (@IdMessage, @Lien);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, fichierMessage);
            fichierMessage.IdLien = (int)id;
        }

        public void DeleteFichierMessage(int lienId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM FichierMessage WHERE Id_Lien = @IdLien";
            connection.Execute(sql, new { IdLien = lienId });
        }
    }
}
