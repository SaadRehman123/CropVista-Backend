using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/inventory")]
    [ApiController]
    public class InventoryStatusController : ControllerBase
    {
        private IConfiguration _config;

        public InventoryStatusController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<InventoryStatus> AddInventory(InventoryStatus inventory)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    InventoryStatusServices inventoryStatusServices = new InventoryStatusServices();
                    string inventoryId = inventoryStatusServices.AddInventory(connection, inventory);

                    inventory.inventoryId = inventoryId;

                    return new Result<InventoryStatus>
                    {
                        result = inventory,
                        success = true,
                        message = "ADD_INVENTORY"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<InventoryStatus>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<InventoryStatus> UpdateCropsPlan(InventoryStatus inventory, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    InventoryStatusServices inventoryStatusServices = new InventoryStatusServices();
                    inventoryStatusServices.UpdateInventory(connection, inventory, id);

                    inventory.inventoryId = id;

                    return new Result<InventoryStatus>
                    {
                        success = true,
                        result = inventory,
                        message = "UPDATE_INVENTORY"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<InventoryStatus>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getInventory")]
        public Result<List<InventoryStatus>> GetInventoryStatus()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<InventoryStatus> inventories = new List<InventoryStatus>();

                    InventoryStatusServices inventoryStatusServices = new InventoryStatusServices();
                    inventories = inventoryStatusServices.GetInventory(connection);

                    return new Result<List<InventoryStatus>>
                    {
                        result = inventories,
                        success = true,
                        message = "GET_INVENTORY"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<InventoryStatus>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
