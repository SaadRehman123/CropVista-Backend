using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/goodReceipt")]
    [ApiController]
    public class GoodReceiptController : ControllerBase
    {
        private IConfiguration _config;
        public GoodReceiptController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<GoodReceipt> AddGoodReceipt(GoodReceipt goodReceipt)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    GoodReceiptServices goodReceiptServices = new GoodReceiptServices();
                    string gr_Id = goodReceiptServices.AddGoodReceipt(connection, goodReceipt);

                    goodReceipt.gr_Id = gr_Id;

                    return new Result<GoodReceipt>
                    {
                        result = goodReceipt,
                        success = true,
                        message = "ADD_GOOD_RECEIPT"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<GoodReceipt>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{gr_Id}")]
        public Result<GoodReceipt> UpdateGoodReceipt(GoodReceipt goodReceipt, string gr_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    GoodReceiptServices goodReceiptServices = new GoodReceiptServices();
                    goodReceiptServices.UpdateGoodReceipt(connection, goodReceipt, gr_Id);

                    goodReceipt.gr_Id = gr_Id;

                    return new Result<GoodReceipt>
                    {
                        result = goodReceipt,
                        success = true,
                        message = "UPDATE_GOOD_RECEIPT"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<GoodReceipt>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getGoodReceipt/{gr_Id}")]
        public Result<List<GoodReceipt>> GetGoodReceipt(string gr_Id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<GoodReceipt> goodReceipts = new List<GoodReceipt>();

                    GoodReceiptServices goodReceiptServices = new GoodReceiptServices();
                    goodReceipts = goodReceiptServices.GetGoodReceipt(connection, gr_Id);

                    return new Result<List<GoodReceipt>>
                    {
                        result = goodReceipts,
                        success = true,
                        message = "GET_GOOD_RECEIPT"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<GoodReceipt>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

    }
}
