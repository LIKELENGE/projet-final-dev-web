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
    public class DimensionRepository : IDimensionRepository
    {
        private readonly string _connectionString;

        public DimensionRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion a la base de donnees a echoue : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Dimension> GetAllDimensions()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Dimension";
            return connection.Query<Dimension>(sql);
        }

        public Dimension? GetDimensionById(int dimensionId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Dimension WHERE Id_Dimension = @IdDimension";
            return connection.QuerySingleOrDefault<Dimension>(sql, new { IdDimension = dimensionId });
        }

        public Dimension? GetDimensionByAnnonceId(int annonceId)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM Dimension WHERE Id_Annonce = @IdAnnonce";
            return connection.QuerySingleOrDefault<Dimension>(sql, new { IdAnnonce = annonceId });
        }

        public void AddDimension(Dimension dimension)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO Dimension (Id_Annonce, ProfondeurCm, LongueurCm, LargeurCm, PoidsKg)
                                 VALUES (@IdAnnonce, @ProfondeurCm, @LongueurCm, @LargeurCm, @PoidsKg);
                                 SELECT LAST_INSERT_ID();";

            var id = connection.ExecuteScalar<long>(sql, dimension);
            dimension.IdDimension = (int)id;
        }

        public void UpdateDimension(Dimension dimension)
        {
            using var connection = GetConnection();
            const string sql = @"UPDATE Dimension SET
                                    Id_Annonce = @IdAnnonce,
                                    ProfondeurCm = @ProfondeurCm,
                                    LongueurCm = @LongueurCm,
                                    LargeurCm = @LargeurCm,
                                    PoidsKg = @PoidsKg
                                 WHERE Id_Dimension = @IdDimension";

            connection.Execute(sql, dimension);
        }

        public void DeleteDimension(int dimensionId)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM Dimension WHERE Id_Dimension = @IdDimension";
            connection.Execute(sql, new { IdDimension = dimensionId });
        }
    }
}
