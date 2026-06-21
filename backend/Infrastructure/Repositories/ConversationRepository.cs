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
    public class ConversationRepository : IConversationRepository
    {
        private readonly string _connectionString;

        public ConversationRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Conversation> GetAllConversations()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM conversation";
            return connection.Query<Conversation>(sql);
        }

        public Conversation? GetConversationById(int conversationId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM conversation WHERE id_conversation = @IdConversation";
            return connection.QuerySingleOrDefault<Conversation>(sql, new { IdConversation = conversationId });
        }

        public void AddConversation(Conversation conversation)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO conversation (titre, lien_photo) VALUES (@Titre, @LienPhoto); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, new { conversation.Titre, conversation.LienPhoto });
            conversation.IdConversation = (int)id;
        }

        public void UpdateConversation(Conversation conversation)
        {
            using var connection = GetConnection();
            const string sql = "UPDATE conversation SET titre = @Titre, lien_photo = @LienPhoto WHERE id_conversation = @IdConversation";
            connection.Execute(sql, new { conversation.Titre, conversation.LienPhoto, conversation.IdConversation });
        }

        public void DeleteConversation(int conversationId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM conversation WHERE id_conversation = @IdConversation";
            connection.Execute(sql, new { IdConversation = conversationId });
        }
    }
}
