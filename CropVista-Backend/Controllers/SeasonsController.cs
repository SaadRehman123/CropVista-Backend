using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/seasons")]
    [ApiController]
    public class SeasonsController : ControllerBase
    {
        private IConfiguration _config;

        public SeasonsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("getSeasons")]
        public Result<List<Seasons>> GetUsers()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<Seasons> seasonsList = new List<Seasons>();

                    SeasonServices usersServices = new SeasonServices();

                    seasonsList = usersServices.GetSeasons(connection);

                    return new Result<List<Seasons>>
                    {
                        result = seasonsList,
                        success = true,
                        message = "GET_SEASONS"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<Seasons>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
