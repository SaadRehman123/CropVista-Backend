using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace CropVista_Backend.Controllers
{
    [Route("rest/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IConfiguration _config;
        public UserController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("getUsers")]
        public Result<List<Users>> GetUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
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
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
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