using CropVista_Backend.Models;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace CropVista_Backend.Services
{
    public class AuthServices
    {
        public (bool isAuthenticated, int userId) AuthenticateUser(SqlConnection connection, Auth auth)
        {
            int userId = 0;
            bool isAuthenticated = false;

            using (SqlCommand cmd = new SqlCommand("SELECT userId, password FROM users WHERE email = @Email", connection))
            {
                cmd.Parameters.AddWithValue("@Email", auth.email);

                connection.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string hashedPassword = reader["password"].ToString();
                    userId = (int)reader["userId"];

                    if (hashedPassword != null)
                    {
                        isAuthenticated = BCrypt.Net.BCrypt.Verify(auth.password, hashedPassword);
                    }
                }

                connection.Close();
            }

            return (isAuthenticated, userId);
        }

        public Users GetLoggedInUser(SqlConnection connection, int userId)
        {
            Users user = new Users();

            using (SqlCommand command = new SqlCommand("SELECT * FROM users WHERE userId = @UserId", connection))
            {
                command.Parameters.AddWithValue("@UserId", userId);

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        user.userId = Convert.ToInt32(dt.Rows[0]["userId"]);
                        user.name = Convert.ToString(dt.Rows[0]["name"]);
                        user.email = Convert.ToString(dt.Rows[0]["email"]);
                        user.password = Convert.ToString(dt.Rows[0]["password"]);
                    }
                }
            }

            return user;
        }
    }
}
