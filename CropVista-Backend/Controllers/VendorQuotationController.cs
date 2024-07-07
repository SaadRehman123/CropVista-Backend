using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/vendorQuotation")]
    [ApiController]
    public class VendorQuotationController : ControllerBase
    {
        private IConfiguration _config;
        public VendorQuotationController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<VendorQuotation> AddVendorQuotation(VendorQuotation vendorQuotation)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    VendorQuotationServices vendorQuotationServices = new VendorQuotationServices();
                    string vq_Id = vendorQuotationServices.AddVendorQuotation(connection, vendorQuotation);

                    vendorQuotation.vq_Id = vq_Id;

                    return new Result<VendorQuotation>
                    {
                        result = vendorQuotation,
                        success = true,
                        message = "ADD_VENDOR_QUOTATION"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<VendorQuotation>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{vq_Id}")]
        public Result<VendorQuotation> UpdateVendorQuotation(VendorQuotation vendorQuotation, string vq_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    VendorQuotationServices vendorQuotationServices = new VendorQuotationServices();
                    vendorQuotationServices.UpdateVendorQuotation(connection, vendorQuotation, vq_Id);

                    vendorQuotation.vq_Id = vq_Id;

                    return new Result<VendorQuotation>
                    {
                        result = vendorQuotation,
                        success = true,
                        message = "UPDATE_VENDOR_QUOTATION"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<VendorQuotation>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/{vq_Id}")]
        public Result<VendorQuotation> DeleteVendorQuotation(VendorQuotation vendorQuotation, string vq_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    VendorQuotationServices vendorQuotationServices = new VendorQuotationServices();
                    vendorQuotationServices.DeleteVendorQuotation(connection, vendorQuotation, vq_Id);

                    vendorQuotation.vq_Id = vq_Id;

                    return new Result<VendorQuotation>
                    {
                        result = vendorQuotation,
                        success = true,
                        message = "DELETE_VENDOR_QUOTATION"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<VendorQuotation>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getVQ/{vq_Id}")]
        public Result<List<VendorQuotation>> GetVendorQuotation(string vq_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<VendorQuotation> vendorQuotations = new List<VendorQuotation>();

                    VendorQuotationServices vendorQuotationServices = new VendorQuotationServices();
                    vendorQuotations = vendorQuotationServices.GetVendorQuotation(connection, vq_Id);

                    return new Result<List<VendorQuotation>>
                    {
                        result = vendorQuotations,
                        success = true,
                        message = "GET_VENDOR_QUOTATION"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<VendorQuotation>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
