using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly string connectionString = "Data Source=DESKTOP-RO3M9PJ\\SQLEXPRESS;Initial Catalog=cropVista;Integrated Security=True; Encrypt=False;";

        [HttpGet]
        [Route("getUsers")]
        public Result<List<Users>> GetUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    List<Users> userList = new List<Users>();

                    UsersServices usersServices = new UsersServices();

                    userList = usersServices.GetUsers(connection);

                    return new Result<List<Users>>
                    {
                        result = userList,
                        success = true,
                        message = "GET_USERS"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<Users>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("create")]
        public Result<Users> AddUser(Users user)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    UsersServices usersServices = new UsersServices();
                    user = usersServices.AddUser(connection, user);

                    return new Result<Users>
                    {
                        success = true,
                        result = user,
                        message = "ADD_USER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Users>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}