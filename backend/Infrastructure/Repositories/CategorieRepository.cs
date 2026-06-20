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
    public class CategorieRepository : ICategorieRepository
    {
        private readonly string _connectionString;

        public CategorieRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Categorie> GetAllCategories()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM categorie";
            return connection.Query<Categorie>(sql);
        }

        public Categorie? GetCategorieById(int categorieId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM categorie WHERE id_categorie = @IdCategorie";
            return connection.QuerySingleOrDefault<Categorie>(sql, new { IdCategorie = categorieId });
        }

        public void AddCategorie(Categorie categorie)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO categorie (nom_categorie) VALUES (@NomCategorie); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, new { categorie.NomCategorie });
            categorie.IdCategorie = (int)id;
        }

        public void UpdateCategorie(Categorie categorie)
        {
            using var connection = GetConnection();
            const string sql = "UPDATE categorie SET nom_categorie = @NomCategorie WHERE id_categorie = @IdCategorie";
            connection.Execute(sql, new { categorie.NomCategorie, categorie.IdCategorie });
        }

        public void DeleteCategorie(int categorieId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM categorie WHERE id_categorie = @IdCategorie";
            connection.Execute(sql, new { IdCategorie = categorieId });
        }
    }
}
