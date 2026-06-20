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
    public class SexeRepository : ISexeRepository
    {
        private readonly string _connectionString;

        public SexeRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new ArgumentNullException(nameof(configuration), "La connexion à la base de données a échoué : 'DefaultConnection' introuvable.");
        }

        private IDbConnection GetConnection() => new MySqlConnection(_connectionString);

        public IEnumerable<Sexe> GetAllSexes()
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM sexe";
            return connection.Query<Sexe>(sql);
        }

        public Sexe? GetSexeById(int codeSexe)
        {
            using var connection = GetConnection();
            const string sql = "SELECT * FROM sexe WHERE code_sexe = @CodeSexe";
            return connection.QuerySingleOrDefault<Sexe>(sql, new { CodeSexe = codeSexe });
        }

        public void AddSexe(Sexe sexe)
        {
            using var connection = GetConnection();
            const string sql = @"INSERT INTO sexe (nom_sexe) VALUES (@NomSexe); SELECT LAST_INSERT_ID();";
            var id = connection.ExecuteScalar<long>(sql, new { sexe.NomSexe });
            sexe.CodeSexe = (int)id;
        }

        public void UpdateSexe(Sexe sexe)
        {
            using var connection = GetConnection();
            const string sql = "UPDATE sexe SET nom_sexe = @NomSexe WHERE code_sexe = @CodeSexe";
            connection.Execute(sql, new { sexe.NomSexe, sexe.CodeSexe });
        }

        public void DeleteSexe(int codeSexe)
        {
            using var connection = GetConnection();
            const string sql = "DELETE FROM sexe WHERE code_sexe = @CodeSexe";
            connection.Execute(sql, new { CodeSexe = codeSexe });
        }
    }
}
