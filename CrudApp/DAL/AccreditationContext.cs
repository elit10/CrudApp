using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CrudApp.Models;
using Npgsql;

namespace CrudApp.DAL
{
    public class AccreditationContext : DbContext
    {
        private NpgsqlConnection _connection = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<Accreditation> GetAll()
        {
            List<Accreditation> accreditations = new List<Accreditation>();
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT accreditation_id, engineer_id, mip_id,isaccredited FROM accreditation";
                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Accreditation accreditation = new Accreditation();
                        //accreditation.MIPID = Convert.ToInt32(dr["mip_id"]);
                        accreditation.accreditationID = Convert.ToInt32(dr["accreditation_id"]);
                        accreditation.engineerID = Convert.ToInt32(dr["engineer_id"]);
                        accreditation.isAccredited = Convert.ToBoolean(dr["isaccredited"]);
                        accreditations.Add(accreditation);
                    }
                }
                _connection.Close();
            }
            return accreditations;
        }

        public bool Insert(Accreditation model)
        {
            int insertedRowCount = 0;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO accreditation (accreditation_id,engineer_id,mip_id,isaccredited) VALUES (@accreditation_id, @engineer_id, @mip_id,@isaccredited)";
                    command.Parameters.AddWithValue("@mip_id", model.MIPID);
                    //command.Parameters.AddWithValue("@accreditation_id", model.accreditationID);
                    command.Parameters.AddWithValue("@engineer_id", model.engineerID);
                    command.Parameters.AddWithValue("@isaccredited", model.isAccredited);
                    insertedRowCount = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            return insertedRowCount > 0;
        }

        public Accreditation GetById(int id)
        {
            Accreditation accreditation = null;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT accreditation_id,engineer_id,  mip_id, isaccredited,  FROM accreditation WHERE accreditation_id = @accreditation_id";
                    command.Parameters.AddWithValue("@mip_id", id);
                    NpgsqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        accreditation = new Accreditation();
                        accreditation.MIPID = Convert.ToInt32(dr["mip_id"]);
                        accreditation.engineerID = Convert.ToInt32(dr["engineer_id"]);
                        accreditation.isAccredited = Convert.ToBoolean(dr["isaccredited"]);
                        accreditation.accreditationID = Convert.ToInt32(dr["accreditation_id"]);
                    }
                }
                _connection.Close();
            }
            return accreditation;
        }

        public bool Update(Accreditation model)
        {
            int updatedRowCount = 0;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE accreditation SET mip_id = @mip_id, engineer_id = @engineer_id, isaccredited = @isaccredited, accreditation_id = @accreditation_id WHERE accreditation_id = @accreditation_id";
                    command.Parameters.AddWithValue("@mip_id", model.MIPID);
                    command.Parameters.AddWithValue("@engineer_id", model.engineerID);
                    command.Parameters.AddWithValue("@isaccredited", model.isAccredited);
                    //command.Parameters.AddWithValue("@accreditation_id", model.accreditationID);
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
                    command.CommandText = "DELETE FROM accreditation WHERE accreditation_id = @accreditation_id";
                    command.Parameters.AddWithValue("@mip_id", id);
                    deletedRowCount = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            return deletedRowCount > 0;
        }
    }
}
