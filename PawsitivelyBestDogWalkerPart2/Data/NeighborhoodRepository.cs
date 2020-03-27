using Microsoft.Data.SqlClient;
using PawsitivelyBestDogWalkerPart2.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PawsitivelyBestDogWalkerPart2.Data
{
   public class NeighborhoodRepository
    {
        public SqlConnection Connection
        {
            get
            {
                string _connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=PawsitivelyBestDogWalkerPart2;Integrated Security=True";
                return new SqlConnection(_connectionString);
            }
        }

        public List<Neighborhood> GetAllNeighborhoods()
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();

                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Id, Name FROM Neighborhood";

                    SqlDataReader reader = cmd.ExecuteReader();

                    List<Neighborhood> neighborhoods = new List<Neighborhood>();

                    while (reader.Read())
                    {
                        int idColumnPosition = reader.GetOrdinal("Id");
                        int idValue = reader.GetInt32(idColumnPosition);

                        int neighborhoodNameColumnPosition = reader.GetOrdinal("Name");
                        string neighborhoodNameValue = reader.GetString(neighborhoodNameColumnPosition);

                        Neighborhood neighborhood = new Neighborhood
                        {
                            Id = idValue,
                            Name = neighborhoodNameValue
                        };

                        neighborhoods.Add(neighborhood);
                    }

                    reader.Close();

                    return neighborhoods;
                }
            }
        }

        public Neighborhood GetNeighborhoodById(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT Name FROM Neighborhood WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    SqlDataReader reader = cmd.ExecuteReader();

                    Neighborhood neighborhood = null;

                    if (reader.Read())
                    {
                        neighborhood = new Neighborhood
                        {
                            Id = id,
                            Name = reader.GetString(reader.GetOrdinal("Name"))
                        };
                    }

                    reader.Close();

                    return neighborhood;
                }
            }
        }

        public void AddNeighborhood(Neighborhood neighborhood)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {

                    cmd.CommandText = "INSERT INTO Neighborhood (Name) OUTPUT INSERTED.Id Values (@Name)";
                    cmd.Parameters.Add(new SqlParameter("@Name", neighborhood.Name));

                    int id = (int)cmd.ExecuteScalar();

                    neighborhood.Id = id;
                }
            }

        }

        public void UpdateNeighborhood(int id, Neighborhood neighborhood)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"UPDATE Neighborhood
                                     SET Name = @Name
                                     WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@Name", neighborhood.Name));
                    cmd.Parameters.Add(new SqlParameter("@id", id));

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteNeighborhood(int id)
        {
            using (SqlConnection conn = Connection)
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM Neighborhood WHERE Id = @id";
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
