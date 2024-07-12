using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class InventoryStatusServices
    {
        public string AddInventory(SqlConnection connection, InventoryStatus inventoryStatus)
        {
            string inventoryId = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateInvetoryStatus", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@inventoryItem", inventoryStatus.inventoryItem);
                        cmd.Parameters.AddWithValue("@inventoryQuantity", inventoryStatus.inventoryQuantity);
                        cmd.Parameters.AddWithValue("@inventoryWarehouse", inventoryStatus.inventoryWarehouse);

                        // Output parameter to capture the generated ID
                        SqlParameter outputParam = new SqlParameter("@inventoryId", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        connection.Open();
                        cmd.ExecuteNonQuery();

                        inventoryId = outputParam.Value.ToString();
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

            return inventoryId;
        }

        public InventoryStatus UpdateInventory(SqlConnection connection, InventoryStatus inventoryStatus, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateInvetoryStatus", connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 2);
                        cmd.Parameters.AddWithValue("@inventoryId", id);
                        cmd.Parameters.AddWithValue("@inventoryItem", inventoryStatus.inventoryItem);
                        cmd.Parameters.AddWithValue("@inventoryQuantity", inventoryStatus.inventoryQuantity);
                        cmd.Parameters.AddWithValue("@inventoryWarehouse", inventoryStatus.inventoryWarehouse);

                        connection.Open();
                        int i = cmd.ExecuteNonQuery();
                        connection.Close();
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

            return inventoryStatus;
        }

        public List<InventoryStatus> GetInventory(SqlConnection connection)
        {
            List<InventoryStatus> inventories = new List<InventoryStatus>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateInvetoryStatus", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@inventoryId", "");
                    command.Parameters.AddWithValue("@inventoryItem", "");
                    command.Parameters.AddWithValue("@inventoryQuantity", "");
                    command.Parameters.AddWithValue("@inventoryWarehouse", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            InventoryStatus item = new InventoryStatus
                            {
                                inventoryId = reader.GetString(reader.GetOrdinal("inventoryId")),
                                inventoryItem = reader.GetString(reader.GetOrdinal("inventoryItem")),
                                inventoryQuantity = reader.GetInt32(reader.GetOrdinal("inventoryQuantity")),
                                inventoryWarehouse = reader.GetString(reader.GetOrdinal("inventoryWarehouse")),
                            };

                            inventories.Add(item);
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

            return inventories;
        }
    }
}
