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
    public class ProduitRepository : IProduitRepository
    {
        private readonly string _connectionString;

        public ProduitRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Produit> GetAllProduits()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Produit";
            return connection.Query<Produit>(sql);
        }

        public Produit? GetProduitByAnnonceId(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Produit WHERE Id_Annonce = @IdAnnonce";
            return connection.QuerySingleOrDefault<Produit>(sql, new { IdAnnonce = annonceId });
        }

        public void AddProduit(Produit produit)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO Produit (Id_Annonce, Id_EtatProduit) VALUES (@IdAnnonce, @IdEtatProduit); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, new { produit.IdAnnonce, produit.IdEtatProduit });
            produit.IdAnnonce = (int)id;
        }

        public void UpdateProduit(Produit produit)
        {
            using var connection = GetConnection();
            const string sql = "UPDATE Produit SET Id_EtatProduit = @IdEtatProduit WHERE Id_Annonce = @IdAnnonce";
            connection.Execute(sql, new { produit.IdEtatProduit, produit.IdAnnonce });
        }

        public void DeleteProduit(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM Produit WHERE Id_Annonce = @IdAnnonce";
            connection.Execute(sql, new { IdAnnonce = annonceId });
        }
    }
}
