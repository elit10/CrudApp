using CrudApp.Models;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CrudApp.DAL
{
    public class MIPContext 
    {
        private NpgsqlConnection _connection = null;

        public static IConfiguration Configuration { get; set; }

        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<MIP> GetAll()
        {
            List<MIP> mips = new List<MIP>();
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT mip_id, mip_name FROM mip";
                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        MIP mip = new MIP();
                        mip.MIPID = Convert.ToInt32(dr["mip_id"]);
                        mip.MIPName = dr["mip_name"].ToString();
                        mips.Add(mip);
                    }
                }
                _connection.Close();
            }
            return mips;
        }

        public bool Insert(MIP model)
        {
            int insertedRowCount = 0;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO mip (mip_name) VALUES (@mip_name)";
                    command.Parameters.AddWithValue("@mip_id", model.MIPID);
                    command.Parameters.AddWithValue("@mip_name", model.MIPName);
                    insertedRowCount = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            return insertedRowCount > 0;
        }

        public MIP GetById(int id)
        {
            MIP mip = null;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT mip_id, mip_name FROM mip WHERE mip_id = @mip_id";
                    command.Parameters.AddWithValue("@mip_id", id);
                    NpgsqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        mip = new MIP();
                        mip.MIPID = Convert.ToInt32(dr["mip_id"]);
                        mip.MIPName = dr["mip_name"].ToString();
                    }
                }
                _connection.Close();
            }
            return mip;
        }

        public bool Update(MIP model)
        {
            int updatedRowCount = 0;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE mip SET mip_id = @mip_id, mip_name = @mip_name WHERE mip_id = @mip_id";
                    command.Parameters.AddWithValue("@mip_id", model.MIPID);
                    command.Parameters.AddWithValue("@mip_name", model.MIPName);
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
                    command.CommandText = "DELETE FROM mip WHERE mip_id = @mip_id";
                    command.Parameters.AddWithValue("@mip_id", id);
                    deletedRowCount = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            return deletedRowCount > 0;
        }
    }
    
}
