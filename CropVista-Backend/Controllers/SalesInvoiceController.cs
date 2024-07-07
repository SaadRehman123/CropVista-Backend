using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/saleInvoice")]
    [ApiController]
    public class SalesInvoiceController : ControllerBase
    {
        private IConfiguration _config;
        public SalesInvoiceController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<SaleInvoice> AddSaleInvoice(SaleInvoice saleInvoice)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    SaleInvoiceServices saleInvoiceServices = new SaleInvoiceServices();
                    string salesInvoice_Id = saleInvoiceServices.AddSaleInvoice(connection, saleInvoice);

                    saleInvoice.salesInvoice_Id = salesInvoice_Id;

                    return new Result<SaleInvoice>
                    {
                        result = saleInvoice,
                        success = true,
                        message = "ADD_SALE_INVOICE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<SaleInvoice>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<SaleInvoice> UpdateSaleInvoice(SaleInvoice saleInvoice, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    SaleInvoiceServices saleInvoiceServices = new SaleInvoiceServices();
                    saleInvoiceServices.UpdateSaleInvoice(connection, saleInvoice, id);

                    saleInvoice.salesInvoice_Id = id;

                    return new Result<SaleInvoice>
                    {
                        result = saleInvoice,
                        success = true,
                        message = "UPDATE_SALE_INVOICE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<SaleInvoice>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getSaleInvoice/{id}")]
        public Result<List<SaleInvoice>> GetSaleInvoice(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<SaleInvoice> saleInvoices = new List<SaleInvoice>();

                    SaleInvoiceServices saleInvoiceServices = new SaleInvoiceServices();
                    saleInvoices = saleInvoiceServices.GetSaleInvoice(connection, id);

                    return new Result<List<SaleInvoice>>
                    {
                        result = saleInvoices,
                        success = true,
                        message = "GET_SALE_INVOICE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<SaleInvoice>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
