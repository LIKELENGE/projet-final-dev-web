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
            const string sql = "SELECT * FROM PhotoAnnonce";
            return connection.Query<PhotoAnnonce>(sql);
        }

        public PhotoAnnonce? GetPhotoAnnonceById(int photoAnnonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM PhotoAnnonce WHERE Id_PhotoAnnonce = @IdPhotoAnnonce";
            return connection.QuerySingleOrDefault<PhotoAnnonce>(sql, new { IdPhotoAnnonce = photoAnnonceId });
        }

        public IEnumerable<PhotoAnnonce> GetPhotosByAnnonceId(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM PhotoAnnonce WHERE Id_Annonce = @IdAnnonce";
            return connection.Query<PhotoAnnonce>(sql, new { IdAnnonce = annonceId });
        }

        public void AddPhotoAnnonce(PhotoAnnonce photoAnnonce)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO PhotoAnnonce (Id_Annonce, Titre, Lien) VALUES (@IdAnnonce, @Titre, @Lien); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, new { photoAnnonce.IdAnnonce, photoAnnonce.Titre, photoAnnonce.Lien });
            photoAnnonce.IdPhotoAnnonce = (int)id;
        }

        public void UpdatePhotoAnnonce(PhotoAnnonce photoAnnonce)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE PhotoAnnonce SET Id_Annonce = @IdAnnonce, Titre = @Titre, Lien = @Lien WHERE Id_PhotoAnnonce = @IdPhotoAnnonce";
            connection.Execute(sql, new { photoAnnonce.IdAnnonce, photoAnnonce.Titre, photoAnnonce.Lien, photoAnnonce.IdPhotoAnnonce });
        }

        public void DeletePhotoAnnonce(int photoAnnonceId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM PhotoAnnonce WHERE Id_PhotoAnnonce = @IdPhotoAnnonce";
            connection.Execute(sql, new { IdPhotoAnnonce = photoAnnonceId });
        }
    }
}
