using CropVista_Backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using CropVista_Backend.Common;
using CropVista_Backend.Services;

namespace CropVista_Backend.Controllers
{
    [Route("rest/stockEntries")]
    [ApiController]
    public class StockEntriesController : ControllerBase
    {
        private IConfiguration _config;

        public StockEntriesController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<StockEntries> AddStockEntry(StockEntries stockEntries)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    StockEntriesServices stockEntriesServices = new StockEntriesServices();
                    string stockEntryId = stockEntriesServices.AddStockEntry(connection, stockEntries);

                    stockEntries.StockEntryId = stockEntryId;

                    return new Result<StockEntries>
                    {
                        result = stockEntries,
                        success = true,
                        message = "ADD_STOCK_ENTRY"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<StockEntries>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getStockEntries")]
        public Result<List<StockEntries>> GetStockEntries()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<StockEntries> stockEntriesList = new List<StockEntries>();

                    StockEntriesServices stockEntriesServices = new StockEntriesServices();
                    stockEntriesList = stockEntriesServices.GetStockEntries(connection);

                    return new Result<List<StockEntries>>
                    {
                        result = stockEntriesList,
                        success = true,
                        message = "GET_STOCK_ENTRIES"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<StockEntries>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
