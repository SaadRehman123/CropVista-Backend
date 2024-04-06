﻿using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class WarehouseServices
    {
        public string AddWarehouse(SqlConnection connection, Warehouse warehouse)
        {
            string wareHouseId = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateWarehouse", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 1);
                    cmd.Parameters.AddWithValue("@name", warehouse.name);
                    cmd.Parameters.AddWithValue("@wrType", warehouse.wrType);
                    cmd.Parameters.AddWithValue("@inactive", warehouse.inactive);
                    cmd.Parameters.AddWithValue("@location", warehouse.location);

                    // Output parameter to capture the generated ID
                    SqlParameter outputParam = new SqlParameter("@wrId", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    wareHouseId = outputParam.Value.ToString();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return wareHouseId;
        }

        public List<Warehouse> AddWarehouses(SqlConnection connection)
        {
            List<Warehouse> warehouses = new List<Warehouse>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateWarehouse", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@wrId", "");
                    command.Parameters.AddWithValue("@name", "");
                    command.Parameters.AddWithValue("@wrType", "");
                    command.Parameters.AddWithValue("@inactive", "");
                    command.Parameters.AddWithValue("@location", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Warehouse warehouse = new Warehouse
                            {
                                wrId = reader.GetString(reader.GetOrdinal("wrId")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                wrType = reader.GetString(reader.GetOrdinal("wrType")),
                                inactive = reader.GetBoolean(reader.GetOrdinal("inactive")),
                                location = reader.GetString(reader.GetOrdinal("location"))
                            };

                            warehouses.Add(warehouse);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return warehouses;
        }
    }
}