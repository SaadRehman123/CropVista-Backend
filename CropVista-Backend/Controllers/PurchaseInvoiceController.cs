using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/purchaseInvoice")]
    [ApiController]
    public class PurchaseInvoiceController : ControllerBase
    {
        private IConfiguration _config;
        public PurchaseInvoiceController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<PurchaseInvoice> AddPurchaseInvoice(PurchaseInvoice purchaseInvoice)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    PurchaseInvoiceServices purchaseInvoiceServices = new PurchaseInvoiceServices();
                    string pi_Id = purchaseInvoiceServices.AddPurchaseInvoice(connection, purchaseInvoice);

                    purchaseInvoice.pi_Id = pi_Id;

                    return new Result<PurchaseInvoice>
                    {
                        result = purchaseInvoice,
                        success = true,
                        message = "ADD_PURCHASE_INVOICE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<PurchaseInvoice>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{pi_Id}")]
        public Result<PurchaseInvoice> UpdatePurchaseInvoice(PurchaseInvoice purchaseInvoice, string pi_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    PurchaseInvoiceServices purchaseInvoiceServices = new PurchaseInvoiceServices();
                    purchaseInvoiceServices.UpdatePurchaseInvoice(connection, purchaseInvoice, pi_Id);

                    purchaseInvoice.pi_Id = pi_Id;

                    return new Result<PurchaseInvoice>
                    {
                        result = purchaseInvoice,
                        success = true,
                        message = "UPDATE_PURCHASE_INVOICE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<PurchaseInvoice>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getPurchaseInvoice/{pi_Id}")]
        public Result<List<PurchaseInvoice>> GetPurchaseInvoice(string pi_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<PurchaseInvoice> purchaseInvoices = new List<PurchaseInvoice>();

                    PurchaseInvoiceServices purchaseInvoiceServices = new PurchaseInvoiceServices();
                    purchaseInvoices = purchaseInvoiceServices.GetPurchaseInvoice(connection, pi_Id);

                    return new Result<List<PurchaseInvoice>>
                    {
                        result = purchaseInvoices,
                        success = true,
                        message = "GET_PURCHASE_INVOICE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<PurchaseInvoice>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
