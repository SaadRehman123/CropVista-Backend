using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/itemMaster")]
    [ApiController]
    public class itemMasterController : ControllerBase
    {
        private IConfiguration _config;
        public itemMasterController(IConfiguration config)
        {
            _config = config;
        }

        [HttpGet]
        [Route("getItemMaster")]
        public Result<List<itemMaster>> GetItemMaster()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<itemMaster> items = new List<itemMaster>();

                    itemMasterServices itemMasterServices = new itemMasterServices();

                    items = itemMasterServices.GetItemMaster(connection);

                    return new Result<List<itemMaster>>
                    {
                        result = items,
                        success = true,
                        message = "GET_ITEM_MASTER"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<itemMaster>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
