using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/crops")]
    [ApiController]
    public class CropsController : ControllerBase
    {
        private IConfiguration _config;

        public CropsController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<Crops> AddCrop(Crops crops)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    CropsServices cropsServices = new CropsServices();
                    crops = cropsServices.AddCrop(connection, crops);

                    return new Result<Crops>
                    {
                        success = true,
                        result = crops,
                        message = "ADD_CROP"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Crops>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getCropsBySeason/{season}")]
        public Result<List<Crops>> GetCropsBySeason(string season)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    CropsServices cropServices = new CropsServices();

                    List<Crops> crops = cropServices.GetCropsBySeason(connection, season);

                    if (crops != null)
                    {
                        return new Result<List<Crops>>
                        {
                            result = crops,
                            success = true,
                            message = "GET_CROPS_BY_SEASON"
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                return new Result<List<Crops>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }

            return new Result<List<Crops>>
            {
                result = null,
                success = false,
                message = "ERROR"
            };
        }
    }
}
