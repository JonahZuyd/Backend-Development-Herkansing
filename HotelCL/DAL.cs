using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComponentHotel.Models;
using Microsoft.Data.SqlClient;

namespace HotelCL
{
    internal class DAL
    {
        private readonly string _connectionString = "Data Source=.;Initial Catalog=HotelDb;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";

        // GET ALL
        public List<Reservering> GetAllReserveringen()
        {
            var reserveringen = new List<Reservering>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT Id, Name, Genre FROM Reservevingen";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        bands.Add(new Band
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                            Genre = reader.GetString(2)
                        });
                    }
                }
            }

            return bands;
        }
        // ADD
        public void AddBand(Band band)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "INSERT INTO Bands (Name, Genre) VALUES (@Name, @Genre)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", band.Name);
                    cmd.Parameters.AddWithValue("@Genre", band.Genre);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // GETBANDBYID
        public Band? GetBandById(int id)
        {
            Band? band = null;

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "SELECT Id, Name, Genre FROM Bands WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            band = new Band
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Genre = reader.GetString(2)
                            };
                        }
                    }
                }
            }

            return band;
        }


        // UPDATE
        public void UpdateBand(Band band)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "UPDATE Bands SET Name = @Name, Genre = @Genre WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Name", band.Name);
                    cmd.Parameters.AddWithValue("@Genre", band.Genre);
                    cmd.Parameters.AddWithValue("@Id", band.Id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // DELETE
        public void DeleteBand(int id)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string sql = "DELETE FROM Bands WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.ExecuteNonQuery();
                }
            }
        }

    }
}
