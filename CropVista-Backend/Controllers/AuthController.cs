using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace CropVista_Backend.Controllers
{

    [Route("rest/authenticateUser")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IConfiguration _config;

        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("login/{email}/{password}")]
        public Result<Auth> Login(string email, string password)
        {
            try
            {
                string GenerateJSONWebToken()
                {
                    // Generate a 256-bit (32-byte) key
                    var keyBytes = new byte[32];
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(keyBytes);
                    }

                    // Convert the byte array to a Base64 string
                    var base64Key = Convert.ToBase64String(keyBytes);

                    // Use the generated key for creating SymmetricSecurityKey
                    var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(base64Key));
                    var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _config["JwtSettings:Issuer"],
                        _config["JwtSettings:Issuer"],
                        expires: DateTime.Now.AddYears(10),
                        signingCredentials: credentials
                    );

                    return new JwtSecurityTokenHandler().WriteToken(token);
                }

                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    AuthServices authService = new AuthServices();
                    (bool isAuthenticated, int userId) = authService.AuthenticateUser(connection, new Auth { email = email, password = password });

                    if (isAuthenticated)
                    {
                        Auth auth = new Auth
                        {
                            userId = userId,
                            email = email,
                            password = password,
                            isAuthorized = true
                        };

                        var token = GenerateJSONWebToken();
                        auth.Token = token;

                        return new Result<Auth>
                        {
                            success = true,
                            result = auth,
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
                                email = email,
                                password = password,
                                isAuthorized = false,
                            },
                            message = "LOGIN_FAILED"
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

        [HttpGet]
        [Route("getLoggedInUser/{userId}")]
        public Result<Users> GetLoggedInUser(int userId)
        {
            try 
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    Users user = new Users();

                    AuthServices authServices = new AuthServices();

                    user = authServices.GetLoggedInUser(connection, userId);

                    if (user.name != null && user.email != null)
                    {
                        return new Result<Users>
                        {
                            result = user,
                            success = true,
                            message = "GET_LOGGED_IN_USER"
                        };
                    }
                    else
                    {
                        return new Result<Users>
                        {
                            result = null,
                            success = false,
                            message = "INVALID_USER"
                        };
                    }
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
