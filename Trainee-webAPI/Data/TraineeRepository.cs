using Microsoft.Data.SqlClient;
using System.Data;
using Trainee_webAPI.Models;


public class TraineeRepository
{
    private readonly string _connectionString;
    public TraineeRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection");
    }

    public List<Trainee> GetAll()
    {
        var list = new List<Trainee>();
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand("usp_GetAllTrainees", connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Trainee
                {
                    TraineeID = Convert.ToInt32(reader["TraineeID"]),
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    Department = reader["Department"].ToString(),
                    JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                    Gender = reader["Gender"].ToString(),
                    Photo = reader["Photo"] as byte[],
                    Password = reader["Password"].ToString()
                });
            }
            connection.Close();
        }
        return list;
    }

    public Trainee GetByEmail(string email)
    {
        Trainee trainee = null;
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand("usp_GetTraineeByEmail", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@Email", email);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                trainee = new Trainee
                {
                    TraineeID = Convert.ToInt32(reader["TraineeID"]),
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    PhoneNumber = reader["PhoneNumber"].ToString(),
                    Department = reader["Department"].ToString(),
                    JoiningDate = Convert.ToDateTime(reader["JoiningDate"]),
                    Gender = reader["Gender"].ToString(),
                    Photo = reader["Photo"] as byte[],
                    Password = reader["Password"].ToString()
                };
            }
        }
        return trainee;
    }

    public void Create(Trainee trainee)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand("usp_CreateTrainee", connection);
            command.CommandType = CommandType.StoredProcedure;

            // Set all parameters required by usp_CreateTrainee
            command.Parameters.AddWithValue("@Name", trainee.Name);
            command.Parameters.AddWithValue("@Email", trainee.Email);
            command.Parameters.AddWithValue("@PhoneNumber", trainee.PhoneNumber);
            command.Parameters.AddWithValue("@Department", trainee.Department);
            command.Parameters.AddWithValue("@JoiningDate", trainee.JoiningDate);
            command.Parameters.AddWithValue("@Gender", trainee.Gender);
            command.Parameters.AddWithValue("@Photo", trainee.Photo ?? (object)DBNull.Value);  // Handle null photo
            command.Parameters.AddWithValue("@Password", trainee.Password);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }


    public void Update(Trainee trainee)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand("usp_UpdateTrainee", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@TraineeID", trainee.TraineeID);
            command.Parameters.AddWithValue("@Name", trainee.Name);
            command.Parameters.AddWithValue("@Email", trainee.Email);
            command.Parameters.AddWithValue("@PhoneNumber", trainee.PhoneNumber);
            command.Parameters.AddWithValue("@Department", trainee.Department);
            command.Parameters.AddWithValue("@JoiningDate", trainee.JoiningDate);
            command.Parameters.AddWithValue("@Gender", trainee.Gender);
            command.Parameters.AddWithValue("@Photo", trainee.Photo ?? (object)DBNull.Value);
            command.Parameters.AddWithValue("@Password", trainee.Password);

            connection.Open();
            command.ExecuteNonQuery();
        }
    }


    public void Delete(string email)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand("usp_DeleteTraineeByEmail", connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@Email", email);
            connection.Open();
            command.ExecuteNonQuery();
        }
    }
}
