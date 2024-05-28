﻿using CropVista_Backend.Common;
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

        [HttpPost]
        [Route("create")]
        public Result<itemMaster> AddItem(itemMaster item)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    itemMasterServices itemMasterServices = new itemMasterServices();
                    string itemMasterId = itemMasterServices.AddItem(connection, item);

                    item.ItemId = itemMasterId;

                    return new Result<itemMaster>
                    {
                        result = item,
                        success = true,
                        message = "ADD_ITEM"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<itemMaster>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
        }

        [HttpPost]
        [Route("update/{id}")]
        public Result<itemMaster> UpdateItem(itemMaster item, string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(_config["ConnectionString:connection"]))
                {
                    itemMasterServices itemMasterServices = new itemMasterServices();
                    itemMasterServices.UpdateItem(connection, item, id);

                    item.ItemId = id;

                    return new Result<itemMaster>
                    {
                        success = true,
                        result = item,
                        message = "UPDATE_ITEM"
                    };
                }
            }
            catch (Exception ex)
            {
                return new Result<itemMaster>
                {
                    result = null,
                    success = false,
                    message = ex.Message
                };
            }
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
