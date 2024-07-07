using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/goodIssue")]
    [ApiController]
    public class GoodIssueController : ControllerBase
    {
        private IConfiguration _config;
        public GoodIssueController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<GoodIssue> AddGoodIssue(GoodIssue goodIssue)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    GoodIssueServices goodIssueServices = new GoodIssueServices();
                    string gi_Id = goodIssueServices.AddGoodIssue(connection, goodIssue);

                    goodIssue.gi_Id = gi_Id;

                    return new Result<GoodIssue>
                    {
                        result = goodIssue,
                        success = true,
                        message = "ADD_GOOD_ISSUE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<GoodIssue>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<GoodIssue> UpdateGoodIssue(GoodIssue goodIssue, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    GoodIssueServices goodIssueServices = new GoodIssueServices();
                    goodIssueServices.UpdateGoodIssue(connection, goodIssue, id);

                    goodIssue.gi_Id = id;

                    return new Result<GoodIssue>
                    {
                        result = goodIssue,
                        success = true,
                        message = "UPDATE_GOOD_ISSUE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<GoodIssue>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpGet]
        [Route("getGoodIssue/{id}")]
        public Result<List<GoodIssue>> GetGoodIssue(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    List<GoodIssue> goodIssues = new List<GoodIssue>();

                    GoodIssueServices goodIssueServices = new GoodIssueServices();
                    goodIssues = goodIssueServices.GetGoodIssue(connection, id);

                    return new Result<List<GoodIssue>>
                    {
                        result = goodIssues,
                        success = true,
                        message = "GET_GOOD_ISSUE"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<GoodIssue>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
