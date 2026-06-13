using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using Infrastructure.models;
using Infrastructure.Repositories.Abstractions;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;

namespace Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly string _connectionString;

        public MessageRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Message> GetAllMessages()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Message";
            return connection.Query<Message>(sql);
        }

        public Message? GetMessageById(int messageId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Message WHERE Id_Message = @IdMessage";
            return connection.QuerySingleOrDefault<Message>(sql, new { IdMessage = messageId });
        }

        public IEnumerable<Message> GetMessagesByConversationId(int conversationId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Message WHERE Id_Conversation = @ConversationId";
            return connection.Query<Message>(sql, new { ConversationId = conversationId });
        }

        public void AddMessage(Message message)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO Message (Id_Conversation, Id_Utilisateur, ContenuMessage, DateHeureMessage)
                                 VALUES (@IdConversation, @IdUtilisateur, @ContenuMessage, @DateHeureMessage);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, new
            {
                IdConversation = message.IdConversation,
                IdUtilisateur = message.IdUtilisateur,
                ContenuMessage = message.ContenuMessage,
                DateHeureMessage = message.DateHeureMessage
            });

            message.IdMessage = (int)id;
        }

        public void UpdateMessage(Message message)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE Message SET
                                    Id_Conversation = @IdConversation,
                                    Id_Utilisateur = @IdUtilisateur,
                                    ContenuMessage = @ContenuMessage,
                                    DateHeureMessage = @DateHeureMessage
                                 WHERE Id_Message = @IdMessage";

            connection.Execute(sql, new
            {
                message.IdConversation,
                message.IdUtilisateur,
                message.ContenuMessage,
                message.DateHeureMessage,
                message.IdMessage
            });
        }

        public void DeleteMessage(int messageId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM Message WHERE Id_Message = @IdMessage";
            connection.Execute(sql, new { IdMessage = messageId });
        }
    }
}
