using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace CropVista_Backend.Services
{
    public class AuthServices
    {
        public (bool isAuthenticated, int id) AuthenticateUser(SqlConnection connection, Auth auth)
        {
            int id = 0;
            bool isAuthenticated = false;

            using (SqlCommand cmd = new SqlCommand("SELECT id, password FROM users WHERE email = @Email", connection))
            {
                cmd.Parameters.AddWithValue("@Email", auth.email);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string hashedPassword = reader["password"].ToString();
                    id = (int)reader["id"];

                    connection.Close();

                    if (hashedPassword != null)
                    {
                        isAuthenticated = BCrypt.Net.BCrypt.Verify(auth.password, hashedPassword);
                    }
                }

            }

            return (isAuthenticated, id);
        }


    }
}
