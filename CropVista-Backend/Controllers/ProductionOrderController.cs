using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/productionOrder")]
    [ApiController]
    public class ProductionOrderController : ControllerBase
    {
        private IConfiguration _config;
        public ProductionOrderController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<ProductionOrder> AddProductionOrder(ProductionOrder productionOrder)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    ProductionOrderServices productionOrderServices = new ProductionOrderServices();
                    string productionOrderId = productionOrderServices.AddProductionOrder(connection, productionOrder);

                    productionOrder.productionOrderId = productionOrderId;

                    return new Result<ProductionOrder>
                    {
                        result = productionOrder,
                        success = true,
                        message = "ADD_PRODUCTION_ORDER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<ProductionOrder>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<ProductionOrder> UpdateProductionOrder(ProductionOrder productionOrder, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    ProductionOrderServices productionOrderServices = new ProductionOrderServices();
                    productionOrderServices.UpdateProductionOrder(connection, productionOrder, id);

                    productionOrder.productionOrderId = id;

                    return new Result<ProductionOrder>
                    {
                        success = true,
                        result = productionOrder,
                        message = "UPDATE_PRODUCTION_ORDER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<ProductionOrder>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public Result<ProductionOrder> DeleteProductionOrder(ProductionOrder productionOrder, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    ProductionOrderServices productionOrderServices = new ProductionOrderServices();
                    productionOrderServices.DeleteProductionOrder(connection, productionOrder, id);

                    productionOrder.productionOrderId = id;

                    return new Result<ProductionOrder>
                    {
                        success = true,
                        result = productionOrder,
                        message = "DELETE_PRODUCTION_ORDER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<ProductionOrder>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getProductionOrder/{productionId}")]
        public Result<List<ProductionOrder>> GetProductionOrder(string productionId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<ProductionOrder> ProductionOrderList = new List<ProductionOrder>();

                    ProductionOrderServices productionOrderServices = new ProductionOrderServices();

                    ProductionOrderList = productionOrderServices.GetProductionOrder(connection, productionId);

                    return new Result<List<ProductionOrder>>
                    {
                        result = ProductionOrderList,
                        success = true,
                        message = "GET_PRODUCTION_ORDER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<ProductionOrder>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
