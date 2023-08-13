using CrudApp.Models;
using Microsoft.Identity.Client;
using System.Data;
using Npgsql;
using Microsoft.Extensions.Configuration;

namespace CrudApp.DAL
{
    public class Employee_DAL
    {
        NpgsqlConnection _connection = null;


        public static IConfiguration Configuration { get; set; }
        private string GetConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json");

            Configuration = builder.Build();
            return Configuration.GetConnectionString("DefaultConnection");
        }

        public List<Employee> GetAll()
        {
            List<Employee> employeeList = new List<Employee>();
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT engineer_id, engineer_name FROM engineer";
                    NpgsqlDataReader dr = command.ExecuteReader();

                    while (dr.Read())
                    {
                        Employee employee = new Employee();
                        employee.Id = Convert.ToInt32(dr["engineer_id"]);
                        employee.FirstName = dr["engineer_name"].ToString();
                        employeeList.Add(employee);
                    }
                }
                _connection.Close();
            }
            return employeeList;
        }

        public bool Insert(Employee model)
        {
            int insertedRowCount = 0;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = @"INSERT INTO engineer (engineer_name) VALUES (@FirstName)";
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
                    insertedRowCount = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            return insertedRowCount > 0;
        }

        public Employee GetById(int id)
        {
            Employee employee = null;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = "SELECT engineer_id, engineer_name FROM engineer WHERE engineer_id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    NpgsqlDataReader dr = command.ExecuteReader();

                    if (dr.Read())
                    {
                        employee = new Employee();
                        employee.Id = Convert.ToInt32(dr["engineer_id"]);
                        employee.FirstName = dr["engineer_name"].ToString();
                    }
                }
                _connection.Close();
            }
            return employee;
        }

        public bool Update(Employee model)
        {
            int updatedRowCount = 0;
            using (_connection = new NpgsqlConnection(GetConnectionString()))
            {
                _connection.Open();
                using (var command = _connection.CreateCommand())
                {
                    command.CommandText = @"UPDATE engineer SET engineer_name = @FirstName WHERE engineer_id = @Id";
                    command.Parameters.AddWithValue("@Id", model.Id);
                    command.Parameters.AddWithValue("@FirstName", model.FirstName);
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
                    command.CommandText = "DELETE FROM engineer WHERE engineer_id = @Id";
                    command.Parameters.AddWithValue("@Id", id);
                    deletedRowCount = command.ExecuteNonQuery();
                }
                _connection.Close();
            }
            return deletedRowCount > 0;
        }
    }
}
