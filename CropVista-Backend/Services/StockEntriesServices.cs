using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class StockEntriesServices
    {
        public string AddStockEntry(SqlConnection connection, StockEntries stockEntries)
        {
            string StockEntryId = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateStockEntries", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 1);
                    cmd.Parameters.AddWithValue("@StockEntryName", stockEntries.StockEntryName);
                    cmd.Parameters.AddWithValue("@StockEntryWarehouse", stockEntries.StockEntryWarehouse);
                    cmd.Parameters.AddWithValue("@StockEntryQuantity", stockEntries.StockEntryQuantity);
                    cmd.Parameters.AddWithValue("@StockEntryTo", stockEntries.StockEntryTo);
                    cmd.Parameters.AddWithValue("@StockEntryDate", DateTime.Parse(stockEntries.StockEntryDate));
                    cmd.Parameters.AddWithValue("@ProductionOrderId", stockEntries.ProductionOrderId); 

                    // Output parameter to capture the generated ID
                    SqlParameter outputParam = new SqlParameter("@StockEntryId", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    StockEntryId = outputParam.Value.ToString();
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

            return StockEntryId;
        }
        public List<StockEntries> GetStockEntries(SqlConnection connection)
        {
            List<StockEntries> stockEntries = new List<StockEntries>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateStockEntries", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@StockEntryId", "");
                    command.Parameters.AddWithValue("@StockEntryName", "");
                    command.Parameters.AddWithValue("@StockEntryWarehouse", "");
                    command.Parameters.AddWithValue("@StockEntryQuantity", "");
                    command.Parameters.AddWithValue("@StockEntryTo", "");
                    command.Parameters.AddWithValue("@StockEntryDate", "");
                    command.Parameters.AddWithValue("@ProductionOrderId", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            StockEntries stock = new StockEntries
                            {
                                StockEntryId = reader.GetString(reader.GetOrdinal("StockEntryId")),
                                StockEntryName = reader.GetString(reader.GetOrdinal("StockEntryName")),
                                StockEntryWarehouse = reader.GetString(reader.GetOrdinal("StockEntryWarehouse")),
                                StockEntryQuantity = reader.GetInt32(reader.GetOrdinal("StockEntryQuantity")),
                                StockEntryTo = reader.GetString(reader.GetOrdinal("StockEntryTo")),
                                StockEntryDate = reader.GetDateTime(reader.GetOrdinal("StockEntryDate")).ToString("yyyy-MM-dd"),
                                ProductionOrderId = reader.GetString(reader.GetOrdinal("ProductionOrderId"))
                            };

                            stockEntries.Add(stock);
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

            return stockEntries;
        }
    }
}