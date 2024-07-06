using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class itemMasterServices
    {
        public string AddItem(SqlConnection connection, itemMaster item)
        {
            string itemMasterId = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateItemMaster", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 1);
                    cmd.Parameters.AddWithValue("@itemName", item.ItemName);
                    cmd.Parameters.AddWithValue("@itemType", item.ItemType);
                    cmd.Parameters.AddWithValue("@sellingRate", item.SellingRate);
                    cmd.Parameters.AddWithValue("@valuationRate", item.ValuationRate);
                    cmd.Parameters.AddWithValue("@disable", item.Disable);
                    cmd.Parameters.AddWithValue("@UOM", item.UOM);
                    cmd.Parameters.AddWithValue("@season", item.season);
                    cmd.Parameters.AddWithValue("@warehouseId", item.warehouseId);

                    // Output parameter to capture the generated ID
                    SqlParameter outputParam = new SqlParameter("@itemId", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    itemMasterId = outputParam.Value.ToString();
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

            return itemMasterId;
        }
        public itemMaster UpdateItem(SqlConnection connection, itemMaster item, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateItemMaster", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 2);
                    cmd.Parameters.AddWithValue("@itemId", id);
                    cmd.Parameters.AddWithValue("@itemName", item.ItemName);
                    cmd.Parameters.AddWithValue("@itemType", item.ItemType);
                    cmd.Parameters.AddWithValue("@sellingRate", item.SellingRate);
                    cmd.Parameters.AddWithValue("@valuationRate", item.ValuationRate);
                    cmd.Parameters.AddWithValue("@disable", item.Disable);
                    cmd.Parameters.AddWithValue("@UOM", item.UOM);
                    cmd.Parameters.AddWithValue("@season", item.season);
                    cmd.Parameters.AddWithValue("@warehouseId", item.warehouseId);

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

            return item;
        }
        public List<itemMaster> GetItemMaster(SqlConnection connection)
        {
            List<itemMaster> items = new List<itemMaster>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateItemMaster", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@itemId", "");
                    command.Parameters.AddWithValue("@itemName", "");
                    command.Parameters.AddWithValue("@itemType", "");
                    command.Parameters.AddWithValue("@sellingRate", "");
                    command.Parameters.AddWithValue("@valuationRate", "");
                    command.Parameters.AddWithValue("@disable", "");
                    command.Parameters.AddWithValue("@UOM", "");
                    command.Parameters.AddWithValue("@season", "");
                    command.Parameters.AddWithValue("@warehouseId", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            itemMaster item = new itemMaster
                            {
                                ItemId = reader.GetString(reader.GetOrdinal("itemId")),
                                ItemName = reader.GetString(reader.GetOrdinal("itemName")),
                                ItemType = reader.GetString(reader.GetOrdinal("itemType")),
                                SellingRate = (float)reader.GetDouble(reader.GetOrdinal("sellingRate")),
                                ValuationRate = (float)reader.GetDouble(reader.GetOrdinal("valuationRate")),
                                Disable = reader.GetBoolean(reader.GetOrdinal("disable")),
                                UOM = reader.GetString(reader.GetOrdinal("UOM")),
                                season = reader.GetString(reader.GetOrdinal("season")),
                                warehouseId = reader.GetString(reader.GetOrdinal("warehouseId"))
                            };

                            items.Add(item);
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

            return items;
        }
    }
}