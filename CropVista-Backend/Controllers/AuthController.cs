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

        private readonly string connectionString = "Data Source=DESKTOP-RO3M9PJ\\SQLEXPRESS;Initial Catalog=cropVista;Integrated Security=True; Encrypt=False;";

        [HttpPost]
        [Route("login/{email}/{password}")]
        public Result<Auth> Login(string email, string password)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    AuthServices authService = new AuthServices();
                    bool isAuthenticated = authService.AuthenticateUser(connection, new Auth { email = email, password = password });

                    if (isAuthenticated)
                    {
                        Auth auth = new Auth
                        {
                            email = email,
                            password = password,
                            isAuthorized = true
                        };

                        var token = GenerateJSONWebToken();
                        auth.Tokken = token;

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

        private string GenerateJSONWebToken()
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

    }
}
