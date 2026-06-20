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
            const string sql = "SELECT * FROM annonce";
            return connection.Query<Annonce>(sql);
        }

        public Annonce? GetAnnonceById(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM annonce WHERE id_annonce = @IdAnnonce";
            return connection.QuerySingleOrDefault<Annonce>(sql, new { IdAnnonce = annonceId });
        }

        public IEnumerable<Annonce> GetAnnoncesByUtilisateurId(int utilisateurId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM annonce WHERE id_utilisateur = @UtilisateurId";
            return connection.Query<Annonce>(sql, new { UtilisateurId = utilisateurId });
        }

        public IEnumerable<Annonce> GetAnnoncesByCategorieId(int categorieId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM annonce WHERE id_categorie = @CategorieId";
            return connection.Query<Annonce>(sql, new { CategorieId = categorieId });
        }

        public void AddAnnonce(Annonce annonce)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO annonce (id_utilisateur, id_categorie, id_commune, nom, description, prix, date_ajout, derniere_modification)
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
            const string sql = @"UPDATE annonce SET
                                    id_utilisateur = @IdUtilisateur,
                                    id_categorie = @IdCategorie,
                                    id_commune = @IdCommune,
                                    nom = @Nom,
                                    description = @Description,
                                    prix = @Prix,
                                    date_ajout = @DateAjout,
                                    derniere_modification = @DerniereModification
                                 WHERE id_annonce = @IdAnnonce";

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
            const string sql = "DELETE FROM annonce WHERE id_annonce = @IdAnnonce";
            connection.Execute(sql, new { IdAnnonce = annonceId });
        }
    }
}
