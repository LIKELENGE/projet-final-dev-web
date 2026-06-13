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
    public class CommentairePhotoRepository : ICommentairePhotoRepository
    {
        private readonly string _connectionString;

        public CommentairePhotoRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<CommentairePhoto> GetAllCommentairesPhoto()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM CommentairePhoto";
            return connection.Query<CommentairePhoto>(sql);
        }

        public CommentairePhoto? GetCommentairePhotoById(int commentairePhotoId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM CommentairePhoto WHERE Id_CommentairePhoto = @IdCommentairePhoto";
            return connection.QuerySingleOrDefault<CommentairePhoto>(sql, new { IdCommentairePhoto = commentairePhotoId });
        }

        public IEnumerable<CommentairePhoto> GetCommentairesByPhotoAnnonceId(int photoAnnonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM CommentairePhoto WHERE Id_PhotoAnnonce = @IdPhotoAnnonce";
            return connection.Query<CommentairePhoto>(sql, new { IdPhotoAnnonce = photoAnnonceId });
        }

        public void AddCommentairePhoto(CommentairePhoto commentairePhoto)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO CommentairePhoto (Id_PhotoAnnonce, Id_Utilisateur, ContenuCommentairePhoto, DateCommentaire)
                                 VALUES (@IdPhotoAnnonce, @IdUtilisateur, @ContenuCommentairePhoto, @DateCommentaire);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, new
            {
                commentairePhoto.IdPhotoAnnonce,
                commentairePhoto.IdUtilisateur,
                commentairePhoto.ContenuCommentairePhoto,
                commentairePhoto.DateCommentaire
            });

            commentairePhoto.IdCommentairePhoto = (int)id;
        }

        public void UpdateCommentairePhoto(CommentairePhoto commentairePhoto)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE CommentairePhoto SET
                                    Id_PhotoAnnonce = @IdPhotoAnnonce,
                                    Id_Utilisateur = @IdUtilisateur,
                                    ContenuCommentairePhoto = @ContenuCommentairePhoto,
                                    DateCommentaire = @DateCommentaire
                                 WHERE Id_CommentairePhoto = @IdCommentairePhoto";

            connection.Execute(sql, new
            {
                commentairePhoto.IdPhotoAnnonce,
                commentairePhoto.IdUtilisateur,
                commentairePhoto.ContenuCommentairePhoto,
                commentairePhoto.DateCommentaire,
                commentairePhoto.IdCommentairePhoto
            });
        }

        public void DeleteCommentairePhoto(int commentairePhotoId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM CommentairePhoto WHERE Id_CommentairePhoto = @IdCommentairePhoto";
            connection.Execute(sql, new { IdCommentairePhoto = commentairePhotoId });
        }
    }
}
