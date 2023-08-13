using CrudApp.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.IO;

namespace CrudApp.DAL
{
    public class LearningPathContext
    {
        private NpgsqlConnection _connection = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<LearningPathModel> GetAll()
        {
            List<LearningPathModel> learningPaths = new List<LearningPathModel>();
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT learningpath_id, mip_id, name FROM learningpath";
                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        LearningPathModel learningPath = new LearningPathModel();
                        learningPath.LPID = Convert.ToInt32(dr["learningpath_id"]);
                        learningPath.MIPID = Convert.ToInt32(dr["mip_id"]);
                        learningPath.LPName = dr["name"].ToString();
                        learningPaths.Add(learningPath);
                    }
                }
                _connection.Close();
            }
            return learningPaths;
        }

        public bool Insert(LearningPathModel model)
        {
            int insertedRowCount = 0;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO learningpath (mip_id, name) VALUES (@mip_id, @name)";
                    command.Parameters.AddWithValue("@mip_id", model.MIPID);
                    command.Parameters.AddWithValue("@name", model.LPName);
                    insertedRowCount = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            return insertedRowCount > 0;
        }

        public LearningPathModel GetById(int id)
        {
            LearningPathModel learningPath = null;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT learningpath_id, mip_id, name FROM learningpath WHERE learningpath_id = @learningpath_id";
                    command.Parameters.AddWithValue("@learningpath_id", id);
                    NpgsqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        learningPath = new LearningPathModel();
                        learningPath.LPID = Convert.ToInt32(dr["learningpath_id"]);
                        learningPath.MIPID = Convert.ToInt32(dr["mip_id"]);
                        learningPath.LPName = dr["name"].ToString();
                    }
                }
                _connection.Close();
            }
            return learningPath;
        }

        public bool Update(LearningPathModel model)
        {
            int updatedRowCount = 0;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE learningpath SET mip_id = @mip_id, name = @name WHERE learningpath_id = @learningpath_id";
                    command.Parameters.AddWithValue("@learningpath_id", model.LPID);
                    command.Parameters.AddWithValue("@mip_id", model.MIPID);
                    command.Parameters.AddWithValue("@name", model.LPName);
                    updatedRowCount = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            return updatedRowCount > 0;
        }

        public bool Delete(int id)
        {
            int deletedRowCount = 0;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM learningpath WHERE learningpath_id = @learningpath_id";
                    command.Parameters.AddWithValue("@learningpath_id", id);
                    deletedRowCount = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            return deletedRowCount > 0;
        }
    }
}
