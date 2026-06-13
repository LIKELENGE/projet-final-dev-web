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
    public class CommentaireAnnonceRepository : ICommentaireAnnonceRepository
    {
        private readonly string _connectionString;

        public CommentaireAnnonceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<CommentaireAnnonce> GetAllCommentairesAnnonce()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM CommentaireAnnonce";
            return connection.Query<CommentaireAnnonce>(sql);
        }

        public CommentaireAnnonce? GetCommentaireAnnonceById(int commentaireAnnonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM CommentaireAnnonce WHERE Id_CommentaireAnnonce = @IdCommentaireAnnonce";
            return connection.QuerySingleOrDefault<CommentaireAnnonce>(sql, new { IdCommentaireAnnonce = commentaireAnnonceId });
        }

        public IEnumerable<CommentaireAnnonce> GetCommentairesByAnnonceId(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM CommentaireAnnonce WHERE Id_Annonce = @IdAnnonce";
            return connection.Query<CommentaireAnnonce>(sql, new { IdAnnonce = annonceId });
        }

        public void AddCommentaireAnnonce(CommentaireAnnonce commentaireAnnonce)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO CommentaireAnnonce (Id_Annonce, Id_Utilisateur, ContenuCommentaireAnnonce, DateCommentaire)
                                 VALUES (@IdAnnonce, @IdUtilisateur, @ContenuCommentaireAnnonce, @DateCommentaire);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, new
            {
                commentaireAnnonce.IdAnnonce,
                commentaireAnnonce.IdUtilisateur,
                commentaireAnnonce.ContenuCommentaireAnnonce,
                commentaireAnnonce.DateCommentaire
            });

            commentaireAnnonce.IdCommentaireAnnonce = (int)id;
        }

        public void UpdateCommentaireAnnonce(CommentaireAnnonce commentaireAnnonce)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE CommentaireAnnonce SET
                                    Id_Annonce = @IdAnnonce,
                                    Id_Utilisateur = @IdUtilisateur,
                                    ContenuCommentaireAnnonce = @ContenuCommentaireAnnonce,
                                    DateCommentaire = @DateCommentaire
                                 WHERE Id_CommentaireAnnonce = @IdCommentaireAnnonce";

            connection.Execute(sql, new
            {
                commentaireAnnonce.IdAnnonce,
                commentaireAnnonce.IdUtilisateur,
                commentaireAnnonce.ContenuCommentaireAnnonce,
                commentaireAnnonce.DateCommentaire,
                commentaireAnnonce.IdCommentaireAnnonce
            });
        }

        public void DeleteCommentaireAnnonce(int commentaireAnnonceId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM CommentaireAnnonce WHERE Id_CommentaireAnnonce = @IdCommentaireAnnonce";
            connection.Execute(sql, new { IdCommentaireAnnonce = commentaireAnnonceId });
        }
    }
}
