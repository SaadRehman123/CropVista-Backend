using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/customer")]
    [ApiController]
    public class CustomerMasterController : ControllerBase
    {
        private IConfiguration _config;
        public CustomerMasterController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<CustomerMaster> AddCustomer(CustomerMaster customer)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    CustomerMasterServices customerMasterServices = new CustomerMasterServices();
                    string customerMasterId = customerMasterServices.AddCustomer(connection, customer);

                    customer.customerId = customerMasterId;

                    return new Result<CustomerMaster>
                    {
                        result = customer,
                        success = true,
                        message = "ADD_CUSTOMER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<CustomerMaster>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<CustomerMaster> UpdateCustomer(CustomerMaster customer, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    CustomerMasterServices customerMasterServices = new CustomerMasterServices();
                    customerMasterServices.UpdateCustomer(connection, customer, id);

                    customer.customerId = id;

                    return new Result<CustomerMaster>
                    {
                        result = customer,
                        success = true,
                        message = "UPDATE_CUSTOMER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<CustomerMaster>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public Result<CustomerMaster> DeleteCustomer(CustomerMaster customer, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    CustomerMasterServices customerMasterServices = new CustomerMasterServices();
                    customerMasterServices.DeleteCustomer(connection, customer, id);

                    customer.customerId = id;

                    return new Result<CustomerMaster>
                    {
                        result = customer,
                        success = true,
                        message = "DELETE_CUSTOMER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<CustomerMaster>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("master")]
        public Result<List<CustomerMaster>> GetCustomerMaster()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<CustomerMaster> items = new List<CustomerMaster>();

                    CustomerMasterServices customerMasterServices = new CustomerMasterServices();
                    items = customerMasterServices.GetCustomerMaster(connection);

                    return new Result<List<CustomerMaster>>
                    {
                        result = items,
                        success = true,
                        message = "GET_CUSTOMER_MASTER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<CustomerMaster>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
