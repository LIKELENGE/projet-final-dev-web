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
    public class AnnonceRepository : IAnnonceRepository
    {
        private readonly string _connectionString;

        public AnnonceRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Annonce> GetAllAnnonces()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Annonce";
            return connection.Query<Annonce>(sql);
        }

        public Annonce? GetAnnonceById(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Annonce WHERE Id_Annonce = @IdAnnonce";
            return connection.QuerySingleOrDefault<Annonce>(sql, new { IdAnnonce = annonceId });
        }

        public IEnumerable<Annonce> GetAnnoncesByUtilisateurId(int utilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Annonce WHERE Id_Utilisateur = @UtilisateurId";
            return connection.Query<Annonce>(sql, new { UtilisateurId = utilisateurId });
        }

        public IEnumerable<Annonce> GetAnnoncesByCategorieId(int categorieId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Annonce WHERE Id_Categorie = @CategorieId";
            return connection.Query<Annonce>(sql, new { CategorieId = categorieId });
        }

        public void AddAnnonce(Annonce annonce)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO Annonce (Id_Utilisateur, Id_Categorie, Id_Commune, Nom, Description, Prix, DateAjout, DerniereModification)
                                 VALUES (@IdUtilisateur, @IdCategorie, @IdCommune, @Nom, @Description, @Prix, @DateAjout, @DerniereModification);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, new
            {
                IdUtilisateur = annonce.IdUtilisateur,
                IdCategorie = annonce.IdCategorie,
                IdCommune = annonce.IdCommune,
                Nom = annonce.Nom,
                Description = annonce.Description,
                Prix = annonce.Prix,
                DateAjout = annonce.DateAjout,
                DerniereModification = annonce.DerniereModification
            });

            annonce.IdAnnonce = (int)id;
        }

        public void UpdateAnnonce(Annonce annonce)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE Annonce SET
                                    Id_Utilisateur = @IdUtilisateur,
                                    Id_Categorie = @IdCategorie,
                                    Id_Commune = @IdCommune,
                                    Nom = @Nom,
                                    Description = @Description,
                                    Prix = @Prix,
                                    DateAjout = @DateAjout,
                                    DerniereModification = @DerniereModification
                                 WHERE Id_Annonce = @IdAnnonce";

            connection.Execute(sql, new
            {
                annonce.IdUtilisateur,
                annonce.IdCategorie,
                annonce.IdCommune,
                annonce.Nom,
                annonce.Description,
                annonce.Prix,
                annonce.DateAjout,
                annonce.DerniereModification,
                annonce.IdAnnonce
            });
        }

        public void DeleteAnnonce(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM Annonce WHERE Id_Annonce = @IdAnnonce";
            connection.Execute(sql, new { IdAnnonce = annonceId });
        }
    }
}
