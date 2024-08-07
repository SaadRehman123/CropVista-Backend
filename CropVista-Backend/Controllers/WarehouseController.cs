﻿using CropVista_Backend.Common;
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
        public Result<Warehouse> AddWarehouse(Warehouse warehouse)
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

        [HttpPost]
        [Route("update/{id}")]
        public Result<Warehouse> UpdateWarehoues(Warehouse warehouse, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    WarehouseServices warehouseServices= new WarehouseServices();
                    warehouseServices.UpdateWarehoues(connection, warehouse, id);

                    warehouse.wrId = id;

                    return new Result<Warehouse>
                    {
                        success = true,
                        result = warehouse,
                        message = "UPDATE_WAREHOUSE"
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

        [HttpPost]
        [Route("delete/{id}")]
        public Result<Warehouse> DeleteWarehouse(Warehouse warehouse, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    WarehouseServices warehouseServices = new WarehouseServices();
                    warehouseServices.DeleteWarehouse(connection, warehouse, id);

                    warehouse.wrId = id;

                    return new Result<Warehouse>
                    {
                        success = true,
                        result = warehouse,
                        message = "DELETE_WAREHOUSE"
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

        [HttpGet]
        [Route("getWarehouses")]
        public Result<List<Warehouse>> GetWareHouses()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<Warehouse> warehouses = new List<Warehouse>();

                    WarehouseServices warehouseServices = new WarehouseServices();

                    warehouses = warehouseServices.GetWarehouses(connection);

                    return new Result<List<Warehouse>>
                    {
                        result = warehouses,
                        success = true,
                        message = "GET_WAREHOUSE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<Warehouse>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
