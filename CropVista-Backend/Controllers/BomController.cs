using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/bom")]
    [ApiController]
    public class BomController : ControllerBase
    {
        private IConfiguration _config;
        public BomController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<Bom> AddBom(Bom bom)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    BomServices bomServices = new BomServices();
                    string bid = bomServices.AddBom(connection, bom);

                    bom.BID = bid;

                    return new Result<Bom>
                    {
                        result = bom,
                        success = true,
                        message = "ADD_BOM"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Bom>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<Bom> UpdateBom(Bom bom, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    BomServices bomServices = new BomServices();
                    bomServices.UpdateBom(connection, bom, id);

                    bom.BID = id;

                    return new Result<Bom>
                    {
                        success = true,
                        result = bom,
                        message = "UPDATE_BOM"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Bom>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public Result<Bom> DeleteBom(Bom bom, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    BomServices bomServices = new BomServices();
                    bomServices.DeleteBom(connection, bom, id);

                    bom.BID = id;

                    return new Result<Bom>
                    {
                        success = true,
                        result = bom,
                        message = "DELETE_BOM"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<Bom>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getBom/{itemBID}")]
        public Result<List<Bom>> GetBom(string itemBID)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<Bom> bomList = new List<Bom>();

                    BomServices bomServices = new BomServices();

                    bomList = bomServices.GetBom(connection, itemBID);

                    return new Result<List<Bom>>
                    {
                        result = bomList,
                        success = true,
                        message = "GET_BOM"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<Bom>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
