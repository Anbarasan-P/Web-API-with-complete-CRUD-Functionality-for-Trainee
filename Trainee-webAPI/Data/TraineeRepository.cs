using Microsoft.Data.SqlClient;
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
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("usp_GetAllTrainees", con);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
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
        }
        return list;
    }

    public Trainee GetByEmail(string email)
    {
        Trainee trainee = null;
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM Trainees WHERE Email = @Email", con);
            cmd.Parameters.AddWithValue("@Email", email);
            con.Open();
            SqlDataReader reader = cmd.ExecuteReader();
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
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("usp_InsertTrainee", con);
            cmd.Parameters.AddWithValue("@Name", trainee.Name);
            cmd.Parameters.AddWithValue("@Email", trainee.Email);
            cmd.Parameters.AddWithValue("@PhoneNumber", trainee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Department", trainee.Department);
            cmd.Parameters.AddWithValue("@JoiningDate", trainee.JoiningDate);
            cmd.Parameters.AddWithValue("@Gender", trainee.Gender);
            cmd.Parameters.AddWithValue("@Photo", trainee.Photo ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Password", trainee.Password);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void Update(Trainee trainee)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("usp_UpdateTrainee", con);
            cmd.Parameters.AddWithValue("@Name", trainee.Name);
            cmd.Parameters.AddWithValue("@PhoneNumber", trainee.PhoneNumber);
            cmd.Parameters.AddWithValue("@Department", trainee.Department);
            cmd.Parameters.AddWithValue("@JoiningDate", trainee.JoiningDate);
            cmd.Parameters.AddWithValue("@Gender", trainee.Gender);
            cmd.Parameters.AddWithValue("@Photo", trainee.Photo ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Password", trainee.Password);
            cmd.Parameters.AddWithValue("@Email", trainee.Email);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }

    public void Delete(string email)
    {
        using (SqlConnection con = new SqlConnection(_connectionString))
        {
            SqlCommand cmd = new SqlCommand("DELETE FROM Trainees WHERE Email=@Email", con);
            cmd.Parameters.AddWithValue("@Email", email);
            con.Open();
            cmd.ExecuteNonQuery();
        }
    }
}
