using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/authenticateUserProfile")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        string connectionString = "Data Source=DESKTOP-RO3M9PJ\\SQLEXPRESS;Initial Catalog=cropVista;Integrated Security=True; Encrypt=False;";

        [HttpPost]
        [Route("login/{email},{password}")]
        public Result<Auth> Login(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    AuthServices authService = new AuthServices();
                    (bool isAuthenticated, int id) = authService.AuthenticateUser(connection, new Auth { email = email, password = password });

                    if (isAuthenticated)
                    {
                        return new Result<Auth>
                        {
                            success = true,
                            result = new Auth
                            {
                                id = id,
                                email = email,
                                password = password,
                                isAuthorized = true,
                            },
                            message = "LOGIN_SUCCESS"
                        };
                    }
                    else
                    {
                        return new Result<Auth>
                        {
                            success = false,
                            result = new Auth
                            {
                                id = id,
                                email = email,
                                password = password,
                                isAuthorized = false,
                            },
                            message = "INVALID_CREDENTIALS"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Result<Auth>
                {
                    success = false,
                    result = null,
                    message = ex.Message
                };
            }
        }
    }
}
