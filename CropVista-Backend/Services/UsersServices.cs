using CropVista_Backend.Models;
using System.Data;
using System.Data.SqlClient;

namespace CropVista_Backend.Services
{
    public class UsersServices
    {
        public List<Users> GetUsers(SqlConnection connection)
        {
            List<Users> usersList = new List<Users>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM users", connection))
            {
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Users users = new Users
                        {
                            Id = Convert.ToInt32(dt.Rows[i]["Id"]),
                            name = Convert.ToString(dt.Rows[i]["name"]),
                            email = Convert.ToString(dt.Rows[i]["email"]),
                            password = Convert.ToString(dt.Rows[i]["password"]),
                            isAuthorized = Convert.ToBoolean(dt.Rows[i]["isAuthorized"])
                        };

                        usersList.Add(users);
                    }
                }
            }

            return usersList;
        }

        public Users AddUser(SqlConnection connection, Users user)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO users (name, email, password, isAuthorized) VALUES ('" + user.name + "', '" + user.email + "', '" + user.password + "', '" + false + "')", connection))
            {
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
            }

            return user;
        }
    }
}