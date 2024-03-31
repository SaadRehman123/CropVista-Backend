using CropVista_Backend.Models;
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
    }
}
