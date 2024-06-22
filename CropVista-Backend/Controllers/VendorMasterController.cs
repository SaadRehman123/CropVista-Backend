using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/vendorMaster")]
    [ApiController]
    public class VendorMasterController : ControllerBase
    {
        private IConfiguration _config;
        public VendorMasterController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<VendorMaster> AddVendor(VendorMaster vendor) 
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    VendorMasterServices vendorMasterServices = new VendorMasterServices();
                    string vendorMasterId = vendorMasterServices.AddVendor(connection, vendor);

                    vendor.vendorId = vendorMasterId;

                    return new Result<VendorMaster>
                    {
                        result = vendor,
                        success = true,
                        message = "ADD_VENDOR"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<VendorMaster>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<VendorMaster> UpdateVendor(VendorMaster vendor, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    VendorMasterServices vendorMasterServices = new VendorMasterServices();
                    vendorMasterServices.UpdateVendor(connection, vendor, id);

                    vendor.vendorId = id;

                    return new Result<VendorMaster>
                    {
                        success = true,
                        result = vendor,
                        message = "UPDATE_VENDOR"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<VendorMaster>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public Result<VendorMaster> DeleteVendor(VendorMaster vendor, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    VendorMasterServices vendorMasterServices = new VendorMasterServices();
                    vendorMasterServices.DeleteVendor(connection, vendor, id);

                    vendor.vendorId = id;

                    return new Result<VendorMaster>
                    {
                        success = true,
                        result = vendor,
                        message = "DELETE_VENDOR"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<VendorMaster>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getVendorMaster")]
        public Result<List<VendorMaster>> GetVendorMaster()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<VendorMaster> items = new List<VendorMaster>();

                    VendorMasterServices vendorMasterServices = new VendorMasterServices();

                    items = vendorMasterServices.GetVendorMaster(connection);

                    return new Result<List<VendorMaster>>
                    {
                        result = items,
                        success = true,
                        message = "GET_VENDOR_MASTER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<VendorMaster>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
