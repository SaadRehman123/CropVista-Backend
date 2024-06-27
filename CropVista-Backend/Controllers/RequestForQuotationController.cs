using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/rfq")]
    [ApiController]
    public class RequestForQuotationController : ControllerBase
    {
        private IConfiguration _config;
        public RequestForQuotationController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<RequestForQuotation> AddRequestForQuotation(RequestForQuotation requestForQuotation)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    RequestForQuotationServices requestForQuotationServices = new RequestForQuotationServices();
                    string requestForQuotationId = requestForQuotationServices.AddRequestForQuotation(connection, requestForQuotation);

                    requestForQuotation.rfq_Id = requestForQuotationId;

                    return new Result<RequestForQuotation>
                    {
                        result = requestForQuotation,
                        success = true,
                        message = "ADD_REQUEST_FOR_QUOTATION"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<RequestForQuotation>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("create/rfq_Items/{rfq_Id}")]
        public Result<List<RequestForQuotationItem>> AddRequestForQuotationItems(List<RequestForQuotationItem> requestForQuotationItems, string rfq_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    RequestForQuotationServices requestForQuotationServices = new RequestForQuotationServices();
                    requestForQuotationServices.AddRequestForQuotationItems(connection, requestForQuotationItems, rfq_Id);

                    return new Result<List<RequestForQuotationItem>>
                    {
                        result = requestForQuotationItems,
                        success = true,
                        message = "ADD_REQUEST_FOR_QUOTATION_ITEMS"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<RequestForQuotationItem>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("create/rfq_Vendors/{rfq_Id}")]
        public Result<List<RequestForQuotationVendor>> AddRequestForQuotationVendors(List<RequestForQuotationVendor> requestForQuotationVendor, string rfq_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    RequestForQuotationServices requestForQuotationServices = new RequestForQuotationServices();
                    requestForQuotationServices.AddRequestForQuotationVendors(connection, requestForQuotationVendor, rfq_Id);

                    return new Result<List<RequestForQuotationVendor>>
                    {
                        result = requestForQuotationVendor,
                        success = true,
                        message = "ADD_REQUEST_FOR_QUOTATION_VENDOR"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<RequestForQuotationVendor>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/rfq_Items")]
        public Result<List<RequestForQuotationItem>> DeleteRequestForQuotationItems(List<RequestForQuotationItem> requestForQuotationItems)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    RequestForQuotationServices requestForQuotationServices = new RequestForQuotationServices();
                    requestForQuotationServices.DeleteRequestForQuotationItems(connection, requestForQuotationItems);

                    return new Result<List<RequestForQuotationItem>>
                    {
                        result = requestForQuotationItems,
                        success = true,
                        message = "DELETE_REQUEST_FOR_QUOTATION_ITEMS"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<RequestForQuotationItem>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/rfq_Vendor")]
        public Result<List<RequestForQuotationVendor>> DeleteRequestForQuotationVendor(List<RequestForQuotationVendor> requestForQuotationVendor)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    RequestForQuotationServices requestForQuotationServices = new RequestForQuotationServices();
                    requestForQuotationServices.DeleteRequestForQuotationVendors(connection, requestForQuotationVendor);

                    return new Result<List<RequestForQuotationVendor>>
                    {
                        result = requestForQuotationVendor,
                        success = true,
                        message = "DELETE_REQUEST_FOR_QUOTATION_VENDOR"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<RequestForQuotationVendor>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{rfq_Id}")]
        public Result<RequestForQuotation> UpdateRequestForQuotation(RequestForQuotation requestForQuotation, string rfq_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    RequestForQuotationServices requestForQuotationServices = new RequestForQuotationServices();
                    requestForQuotationServices.UpdateRequestForQuotation(connection, requestForQuotation, rfq_Id);

                    requestForQuotation.rfq_Id = rfq_Id;

                    return new Result<RequestForQuotation>
                    {
                        result = requestForQuotation,
                        success = true,
                        message = "UPDATE_REQUEST_FOR_QUOTATION"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<RequestForQuotation>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getRFQ/{rfq_Id}")]
        public Result<List<RequestForQuotation>> GetRequestForQuotation(string rfq_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<RequestForQuotation> requestForQuotations = new List<RequestForQuotation>();

                    RequestForQuotationServices requestForQuotationServices = new RequestForQuotationServices();
                    requestForQuotations = requestForQuotationServices.GetRequestForQuotation(connection, rfq_Id);

                    return new Result<List<RequestForQuotation>>
                    {
                        result = requestForQuotations,
                        success = true,
                        message = "GET_REQUEST_FOR_QUOTATION"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<RequestForQuotation>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
