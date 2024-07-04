using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/purchaseOrder")]
    [ApiController]
    public class PurchaseOrderController : ControllerBase
    {
        private IConfiguration _config;
        public PurchaseOrderController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<PurchaseOrder> AddPurchaseOrder(PurchaseOrder purchaseOrder)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    PurchaseOrderServices purchaseOrderServices = new PurchaseOrderServices();
                    string pro_Id = purchaseOrderServices.AddPurchaseOrder(connection, purchaseOrder);

                    purchaseOrder.pro_Id = pro_Id;

                    return new Result<PurchaseOrder>
                    {
                        result = purchaseOrder,
                        success = true,
                        message = "ADD_PURCHASE_ORDER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<PurchaseOrder>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getPurchaseOrder/{pro_Id}")]
        public Result<List<PurchaseOrder>> GetPurchaseOrder(string pro_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<PurchaseOrder> purchaseOrders = new List<PurchaseOrder>();

                    PurchaseOrderServices purchaseOrderServices = new PurchaseOrderServices();
                    purchaseOrders = purchaseOrderServices.GetPurchaseOrder(connection, pro_Id);

                    return new Result<List<PurchaseOrder>>
                    {
                        result = purchaseOrders,
                        success = true,
                        message = "GET_PURCHASE_ORDER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<PurchaseOrder>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
