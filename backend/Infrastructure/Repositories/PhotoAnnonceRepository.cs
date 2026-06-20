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
    public class PhotoAnnonceRepository : IPhotoAnnonceRepository
    {
        private readonly string _connectionString;

        public PhotoAnnonceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<PhotoAnnonce> GetAllPhotosAnnonce()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM photo_annonce";
            return connection.Query<PhotoAnnonce>(sql);
        }

        public PhotoAnnonce? GetPhotoAnnonceById(int photoAnnonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM photo_annonce WHERE id_photo_annonce = @IdPhotoAnnonce";
            return connection.QuerySingleOrDefault<PhotoAnnonce>(sql, new { IdPhotoAnnonce = photoAnnonceId });
        }

        public IEnumerable<PhotoAnnonce> GetPhotosByAnnonceId(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM photo_annonce WHERE id_annonce = @IdAnnonce";
            return connection.Query<PhotoAnnonce>(sql, new { IdAnnonce = annonceId });
        }

        public void AddPhotoAnnonce(PhotoAnnonce photoAnnonce)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO photo_annonce (id_annonce, titre, lien) VALUES (@IdAnnonce, @Titre, @Lien); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, new { photoAnnonce.IdAnnonce, photoAnnonce.Titre, photoAnnonce.Lien });
            photoAnnonce.IdPhotoAnnonce = (int)id;
        }

        public void UpdatePhotoAnnonce(PhotoAnnonce photoAnnonce)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE photo_annonce SET id_annonce = @IdAnnonce, titre = @Titre, lien = @Lien WHERE id_photo_annonce = @IdPhotoAnnonce";
            connection.Execute(sql, new { photoAnnonce.IdAnnonce, photoAnnonce.Titre, photoAnnonce.Lien, photoAnnonce.IdPhotoAnnonce });
        }

        public void DeletePhotoAnnonce(int photoAnnonceId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM photo_annonce WHERE id_photo_annonce = @IdPhotoAnnonce";
            connection.Execute(sql, new { IdPhotoAnnonce = photoAnnonceId });
        }
    }
}
