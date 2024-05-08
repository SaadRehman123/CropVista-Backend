using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/bomItemResource")]
    [ApiController]
    public class BomItemResourceController : ControllerBase
    {
        private IConfiguration _config;
        public BomItemResourceController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]  
        [Route("create")]
        public Result<List<itemResource>> AddBomItemResources(List<itemResource> itemResources)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    BomItemResourceServices bomItemResourceServices = new BomItemResourceServices();
                    bomItemResourceServices.AddBomItemResource(connection, itemResources);

                    return new Result<List<itemResource>>
                    {
                        result = itemResources,
                        success = true,
                        message = "ADD_BOM_ITEM_RESOURCE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<itemResource>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<itemResource> UpdateBomItemResources(itemResource itemResource, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    BomItemResourceServices bomItemResourceServices = new BomItemResourceServices();
                    bomItemResourceServices.UpdateBomItemResource(connection, itemResource, id);

                    return new Result<itemResource>
                    {
                        success = true,
                        result = itemResource,
                        message = "UPDATE_BOM_ITEM_RESOURCE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<itemResource>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public Result<itemResource> DeleteBomItemResources(itemResource itemResource, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    BomItemResourceServices bomItemResourceServices = new BomItemResourceServices();
                    bomItemResourceServices.DeleteBomItemResource(connection, itemResource, id);

                    return new Result<itemResource>
                    {
                        success = true,
                        result = itemResource,
                        message = "DELETE_BOM_ITEM_RESOURCE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<itemResource>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
