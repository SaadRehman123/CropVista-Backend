using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class BomItemResourceServices
    {
        public void AddBomItemResource(SqlConnection connection, List<itemResource> itemResources)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("BomItemResource", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    foreach (var item in itemResources)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@BID", item.BID);
                        cmd.Parameters.AddWithValue("@id", item.id);
                        cmd.Parameters.AddWithValue("@name", item.name);
                        cmd.Parameters.AddWithValue("@type", item.type);
                        cmd.Parameters.AddWithValue("@quantity", item.quantity);
                        cmd.Parameters.AddWithValue("@UOM", item.UOM);
                        cmd.Parameters.AddWithValue("@warehouseId", item.warehouseId);
                        cmd.Parameters.AddWithValue("@unitPrice", item.unitPrice);
                        cmd.Parameters.AddWithValue("@routeSequence", item.routeSequence);
                        cmd.Parameters.AddWithValue("@priceList", item.priceList);

                        cmd.ExecuteNonQuery();
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
        }
    }
}
