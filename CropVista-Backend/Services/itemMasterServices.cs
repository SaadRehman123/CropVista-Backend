using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class itemMasterServices
    {
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
