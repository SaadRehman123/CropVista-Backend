using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/saleOrder")]
    [ApiController]
    public class SaleOrderController : ControllerBase
    {
        private IConfiguration _config;
        public SaleOrderController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<SaleOrder> AddSaleOrder(SaleOrder saleOrder)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    SaleOrderServices saleOrderServices = new SaleOrderServices();
                    string id = saleOrderServices.AddSaleOrder(connection, saleOrder);

                    saleOrder.saleOrder_Id = id;

                    return new Result<SaleOrder>
                    {
                        result = saleOrder,
                        success = true,
                        message = "ADD_SALE_ORDER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<SaleOrder>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("create/SO_Items/{id}")]
        public Result<List<SaleOrderItems>> AddSaleOrderItems(List<SaleOrderItems> saleOrderItems, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    SaleOrderServices saleOrderServices = new SaleOrderServices();
                    saleOrderServices.AddSaleOrderItems(connection, saleOrderItems, id);

                    return new Result<List<SaleOrderItems>>
                    {
                        result = saleOrderItems,
                        success = true,
                        message = "ADD_SALE_ORDER_ITEMS"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<SaleOrderItems>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/SO_Items")]
        public Result<List<SaleOrderItems>> DeleteSaleOrderItems(List<SaleOrderItems> saleOrderItems)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    SaleOrderServices saleOrderServices = new SaleOrderServices();
                    saleOrderServices.DeleteSaleOrderItems(connection, saleOrderItems);

                    return new Result<List<SaleOrderItems>>
                    {
                        result = saleOrderItems,
                        success = true,
                        message = "DELETE_SALE_ORDER_ITEMS"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<SaleOrderItems>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<SaleOrder> UpdateSaleOrder(SaleOrder saleOrder, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    SaleOrderServices saleOrderServices = new SaleOrderServices();
                    saleOrderServices.UpdateSaleOrder(connection, saleOrder, id);
                    
                    saleOrder.saleOrder_Id = id;

                    return new Result<SaleOrder>
                    {
                        result = saleOrder,
                        success = true,
                        message = "UPDATE_SALE_ORDER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<SaleOrder>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getSaleOrder/{id}")]
        public Result<List<SaleOrder>> GetSaleOrder(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<SaleOrder> sales = new List<SaleOrder>();

                    SaleOrderServices saleOrderServices = new SaleOrderServices();
                    sales = saleOrderServices.GetSaleOrder(connection, id);

                    return new Result<List<SaleOrder>>
                    {
                        result = sales,
                        success = true,
                        message = "GET_SALE_ORDER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<SaleOrder>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
