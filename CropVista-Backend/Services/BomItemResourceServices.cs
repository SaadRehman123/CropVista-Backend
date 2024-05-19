using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Linq;

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
                        cmd.Parameters.AddWithValue("@quantity", item.Itemquantity);
                        cmd.Parameters.AddWithValue("@UOM", item.UOM);
                        cmd.Parameters.AddWithValue("@warehouseId", item.warehouseId);
                        cmd.Parameters.AddWithValue("@unitPrice", item.unitPrice);
                        cmd.Parameters.AddWithValue("@routeSequence", item.routeSequence);
                        cmd.Parameters.AddWithValue("@itemResourceId", "");

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
        public itemResource UpdateBomItemResource(SqlConnection connection, itemResource itemResource, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("BomItemResource", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 2);
                    cmd.Parameters.AddWithValue("@BID", itemResource.BID);
                    cmd.Parameters.AddWithValue("@id", itemResource.id);
                    cmd.Parameters.AddWithValue("@name", itemResource.name);
                    cmd.Parameters.AddWithValue("@type", itemResource.type);
                    cmd.Parameters.AddWithValue("@quantity", itemResource.Itemquantity);
                    cmd.Parameters.AddWithValue("@UOM", itemResource.UOM);
                    cmd.Parameters.AddWithValue("@warehouseId", itemResource.warehouseId);
                    cmd.Parameters.AddWithValue("@unitPrice", itemResource.unitPrice);
                    cmd.Parameters.AddWithValue("@routeSequence", itemResource.routeSequence);
                    cmd.Parameters.AddWithValue("@itemResourceId", id);

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

            return itemResource;
        }
        public itemResource DeleteBomItemResource(SqlConnection connection, itemResource itemResource, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("BomItemResource", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 3);
                    cmd.Parameters.AddWithValue("@BID", itemResource.BID);
                    cmd.Parameters.AddWithValue("@id", itemResource.id);
                    cmd.Parameters.AddWithValue("@name", itemResource.name);
                    cmd.Parameters.AddWithValue("@type", itemResource.type);
                    cmd.Parameters.AddWithValue("@quantity", itemResource.Itemquantity);
                    cmd.Parameters.AddWithValue("@UOM", itemResource.UOM);
                    cmd.Parameters.AddWithValue("@warehouseId", itemResource.warehouseId);
                    cmd.Parameters.AddWithValue("@unitPrice", itemResource.unitPrice);
                    cmd.Parameters.AddWithValue("@routeSequence", itemResource.routeSequence);
                    cmd.Parameters.AddWithValue("@itemResourceId", id);

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

            return itemResource;
        }
    }
}
