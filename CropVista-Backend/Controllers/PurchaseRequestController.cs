using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Security.Cryptography;

namespace CropVista_Backend.Controllers
{
    [Route("rest/purchaseRequest")]
    [ApiController]
    public class PurchaseRequestController : ControllerBase
    {
        private IConfiguration _config;
        public PurchaseRequestController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<PurchaseRequest> AddPurchaseRequest(PurchaseRequest purchaseRequest)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    PurchaseRequestServices purchaseRequestServices = new PurchaseRequestServices();
                    string purchaseRequestId = purchaseRequestServices.AddPurchaseRequest(connection, purchaseRequest);

                    purchaseRequest.purchaseRequestId = purchaseRequestId;
                    
                    return new Result<PurchaseRequest>
                    {
                        result = purchaseRequest,
                        success = true,
                        message = "ADD_PURCHASE_REQUEST"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<PurchaseRequest>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("create/PR_Items/{purchaseRequestId}")]
        public Result<List<PurchaseRequestItems>> AddPurchaseRequestItems(List<PurchaseRequestItems> purchaseRequestItems, string purchaseRequestId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    PurchaseRequestServices purchaseRequestServices = new PurchaseRequestServices();
                    purchaseRequestServices.AddPurchaseRequestItems(connection, purchaseRequestItems, purchaseRequestId);

                    return new Result<List<PurchaseRequestItems>>
                    {
                        result = purchaseRequestItems,
                        success = true,
                        message = "ADD_PURCHASE_REQUEST_ITEMS"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<PurchaseRequestItems>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/PR_Items")]
        public Result<List<PurchaseRequestItems>> DeletePurchaseRequestItems(List<PurchaseRequestItems> purchaseRequestItems)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    PurchaseRequestServices purchaseRequestServices = new PurchaseRequestServices();
                    purchaseRequestServices.DeletePurchaseRequestItem(connection, purchaseRequestItems);

                    return new Result<List<PurchaseRequestItems>>
                    {
                        result = purchaseRequestItems,
                        success = true,
                        message = "DELETE_PURCHASE_REQUEST_ITEMS"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<PurchaseRequestItems>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{PR_Id}")]
        public Result<PurchaseRequest> UpdatePurchaseRequest(PurchaseRequest purchaseRequest, string PR_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    PurchaseRequestServices purchaseRequestServices = new PurchaseRequestServices();

                    purchaseRequestServices.UpdatePurchaseRequest(connection, purchaseRequest, PR_Id);
                    purchaseRequest.purchaseRequestId = PR_Id;

                    return new Result<PurchaseRequest>
                    {
                        result = purchaseRequest,
                        success = true,
                        message = "UPDATE_PURCHASE_REQUEST"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<PurchaseRequest>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getPurchaseRequest/{PR_Id}")]
        public Result<List<PurchaseRequest>> GetProductionOrder(string PR_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<PurchaseRequest> purchases = new List<PurchaseRequest>();

                    PurchaseRequestServices purchaseRequestServices = new PurchaseRequestServices();

                    purchases = purchaseRequestServices.GetPurchaseRequest(connection, PR_Id);

                    return new Result<List<PurchaseRequest>>
                    {
                        result = purchases,
                        success = true,
                        message = "GET_PURCHASE_REQUEST"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<PurchaseRequest>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
