using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/warehouse")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private IConfiguration _config;
        public WarehouseController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<Warehouse> AddCropsPlan(Warehouse warehouse)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    WarehouseServices warehouseServices = new WarehouseServices();
                    string wareHouseId = warehouseServices.AddWarehouse(connection, warehouse);

                    warehouse.wrId = wareHouseId;

                    return new Result<Warehouse>
                    {
                        result = warehouse,
                        success = true,
                        message = "ADD_WAREHOUSE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Warehouse>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
