using CropVista_Backend.Common;
using CropVista_Backend.Models;
using CropVista_Backend.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace CropVista_Backend.Controllers
{
    [Route("rest/routeStages")]
    [ApiController]
    public class PO_RouteStagesController : ControllerBase
    {
        private IConfiguration _config;
        public PO_RouteStagesController(IConfiguration config)
        {
            _config = config;
        }

        [HttpPost]
        [Route("create")]
        public Result<List<PO_RouteStages>> AddPoRouteStages(List<PO_RouteStages> routeStages)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    PO_RouteStagesServices routeStagesServices = new PO_RouteStagesServices();
                    routeStagesServices.AddPoRouteStages(connection, routeStages);

                    return new Result<List<PO_RouteStages>>
                    {
                        result = routeStages,
                        success = true,
                        message = "ADD_PO_ROUTE_STAGES"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<List<PO_RouteStages>>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<PO_RouteStages> UpdatePoRouteStages(PO_RouteStages routeStages, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    PO_RouteStagesServices routeStagesServices = new PO_RouteStagesServices();
                    routeStagesServices.UpdatePoRouteStages(connection, routeStages, id);

                    return new Result<PO_RouteStages>
                    {
                        success = true,
                        result = routeStages,
                        message = "UPDATE_PO_ROUTE_STAGES"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<PO_RouteStages>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("delete/{id}")]
        public Result<PO_RouteStages> DeletePoRouteStages(PO_RouteStages routeStages, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    PO_RouteStagesServices routeStagesServices = new PO_RouteStagesServices();
                    routeStagesServices.DeletePoRouteStages(connection, routeStages, id);

                    return new Result<PO_RouteStages>
                    {
                        success = true,
                        result = routeStages,
                        message = "DELETE_PO_ROUTE_STAGES"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<PO_RouteStages>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }
    }
}
