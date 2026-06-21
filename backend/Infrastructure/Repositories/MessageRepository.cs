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
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Message> GetAllMessages()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM message";
            return connection.Query<Message>(sql);
        }

        public Message? GetMessageById(int messageId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM message WHERE id_message = @IdMessage";
            return connection.QuerySingleOrDefault<Message>(sql, new { IdMessage = messageId });
        }

        public IEnumerable<Message> GetMessagesByConversationId(int conversationId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM message WHERE id_conversation = @ConversationId";
            return connection.Query<Message>(sql, new { ConversationId = conversationId });
        }

        public void AddMessage(Message message)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO message (id_conversation, id_utilisateur, contenu_message, date_heure_message)
                                 VALUES (@IdConversation, @IdUtilisateur, @ContenuMessage, @DateHeureMessage);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, new
            {
                message.IdConversation,
                message.IdUtilisateur,
                message.ContenuMessage,
                message.DateHeureMessage
            });

            message.IdMessage = (int)id;
        }

        public void UpdateMessage(Message message)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE message SET
                                    id_conversation = @IdConversation,
                                    id_utilisateur = @IdUtilisateur,
                                    contenu_message = @ContenuMessage,
                                    date_heure_message = @DateHeureMessage
                                 WHERE id_message = @IdMessage";

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
            const string sql = "DELETE FROM message WHERE id_message = @IdMessage";
            connection.Execute(sql, new { IdMessage = messageId });
        }
    }
}
