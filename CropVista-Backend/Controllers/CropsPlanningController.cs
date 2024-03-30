using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/cropsplan")]
    [ApiController]
    public class CropsPlanningController : ControllerBase
    {
        private IConfiguration _config;
        public CropsPlanningController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("getCropsPlan")]
        public Result<List<CropsPlanning>> GetPlannedCrops()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<CropsPlanning> plannedCropsList = new List<CropsPlanning>();

                    CropsPlanningServices cropsPlanningServices = new CropsPlanningServices();

                    plannedCropsList = cropsPlanningServices.GetPlannedCrops(connection);

                    return new Result<List<CropsPlanning>>
                    {
                        result = plannedCropsList,
                        success = true,
                        message = "GET_PLANNED_CROPS"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<CropsPlanning>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("create")]
        public Result<CropsPlanning> AddCropsPlan(CropsPlanning cropsPlanning)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    CropsPlanningServices cropsPlanningServices = new CropsPlanningServices();
                    int generatedId = cropsPlanningServices.AddCropsPlan(connection, cropsPlanning);

                    cropsPlanning.id = generatedId;

                    return new Result<CropsPlanning>
                    {
                        success = true,
                        result = cropsPlanning,
                        message = "ADD_CROPS_PLAN"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<CropsPlanning>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }


        [HttpPost]
        [Route("update/{id}")]
        public Result<CropsPlanning> UpdateCropsPlan(CropsPlanning cropsPlanning, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    CropsPlanningServices cropsPlanningServices = new CropsPlanningServices();
                    cropsPlanningServices.UpdateCropsPlan(connection, cropsPlanning, id);

                    cropsPlanning.id = id;

                    return new Result<CropsPlanning>
                    {
                        success = true,
                        result = cropsPlanning,
                        message = "UPDATE_CROPS_PLAN"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<CropsPlanning>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public Result<CropsPlanning> DeleteCropsPlan(CropsPlanning cropsPlanning, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    CropsPlanningServices cropsPlanningServices = new CropsPlanningServices();
                    cropsPlanningServices.DeleteCropsPlan(connection, cropsPlanning, id);

                    cropsPlanning.id = id;

                    return new Result<CropsPlanning>
                    {
                        success = true,
                        result = cropsPlanning,
                        message = "DELETE_CROPS_PLAN"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<CropsPlanning>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
