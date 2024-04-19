using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/resource")]
    [ApiController]
    public class ResourceController : ControllerBase
    {
        private IConfiguration _config;
        public ResourceController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<Resource> AddResource(Resource resource)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    ResourceServices resourceServices = new ResourceServices();
                    string resourceId = resourceServices.AddResource(connection, resource);

                    resource.rId = resourceId;

                    return new Result<Resource>
                    {
                        result = resource,
                        success = true,
                        message = "ADD_RESOURCE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Resource>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<Resource> UpdateResource(Resource resource, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    ResourceServices resourceServices = new ResourceServices();
                    resourceServices.UpdateResource(connection, resource, id);

                    resource.rId = id;

                    return new Result<Resource>
                    {
                        success = true,
                        result = resource,
                        message = "UPDATE_RESOURCE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Resource>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public Result<Resource> DeleteWarehouse(Resource resource, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    ResourceServices resourceServices = new ResourceServices();
                    resourceServices.DeleteResource(connection, resource, id);

                    resource.rId = id;

                    return new Result<Resource>
                    {
                        success = true,
                        result = resource,
                        message = "DELETE_RESOURCE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Resource>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getResources")]
        public Result<List<Resource>> GetResources()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<Resource> resources = new List<Resource>();

                    ResourceServices resourceServices = new ResourceServices();

                    resources = resourceServices.GetResources(connection);

                    return new Result<List<Resource>>
                    {
                        result = resources,
                        success = true,
                        message = "GET_RESOURCES"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<Resource>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
