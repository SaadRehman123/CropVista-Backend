using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class PO_RouteStagesServices
    {
        public void AddPoRouteStages(SqlConnection connection, List<PO_RouteStages> routeStages)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("ProductionOrderRouteStages", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    foreach (var item in routeStages)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@PO_productionOrderId", item.PO_productionOrderId);
                        cmd.Parameters.AddWithValue("@PO_RouteStage", item.PO_RouteStage);
                        cmd.Parameters.AddWithValue("@PO_Type", item.PO_Type);
                        cmd.Parameters.AddWithValue("@PO_ItemNo", item.PO_ItemNo);
                        cmd.Parameters.AddWithValue("@PO_ItemDescription", item.PO_ItemDescription);
                        cmd.Parameters.AddWithValue("@PO_Quantity", item.PO_Quantity);
                        cmd.Parameters.AddWithValue("@PO_Uom", item.PO_Uom);
                        cmd.Parameters.AddWithValue("@PO_WarehouseId", item.PO_WarehouseId);
                        cmd.Parameters.AddWithValue("@PO_UnitPrice", item.PO_UnitPrice);
                        cmd.Parameters.AddWithValue("@PO_Total", item.PO_Total);
                        cmd.Parameters.AddWithValue("@PO_Status", item.PO_Status);

                        SqlParameter outputParam = new SqlParameter("@PO_RouteStageId", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        item.PO_RouteStageId = outputParam.Value.ToString();
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
        public PO_RouteStages UpdatePoRouteStages(SqlConnection connection, PO_RouteStages routeStages, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("ProductionOrderRouteStages", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 2);
                    cmd.Parameters.AddWithValue("@PO_productionOrderId", routeStages.PO_productionOrderId);
                    cmd.Parameters.AddWithValue("@PO_RouteStageId", id);
                    cmd.Parameters.AddWithValue("@PO_RouteStage", routeStages.PO_RouteStage);
                    cmd.Parameters.AddWithValue("@PO_Type", routeStages.PO_Type);
                    cmd.Parameters.AddWithValue("@PO_ItemNo", routeStages.PO_ItemNo);
                    cmd.Parameters.AddWithValue("@PO_ItemDescription", routeStages.PO_ItemDescription);
                    cmd.Parameters.AddWithValue("@PO_Quantity", routeStages.PO_Quantity);
                    cmd.Parameters.AddWithValue("@PO_Uom", routeStages.PO_Uom);
                    cmd.Parameters.AddWithValue("@PO_WarehouseId", routeStages.PO_WarehouseId);
                    cmd.Parameters.AddWithValue("@PO_UnitPrice", routeStages.PO_UnitPrice);
                    cmd.Parameters.AddWithValue("@PO_Total", routeStages.PO_Total);
                    cmd.Parameters.AddWithValue("@PO_Status", routeStages.PO_Status);

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

            return routeStages;
        }
        public PO_RouteStages DeletePoRouteStages(SqlConnection connection, PO_RouteStages routeStages, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("ProductionOrderRouteStages", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 3);
                    cmd.Parameters.AddWithValue("@PO_productionOrderId", routeStages.PO_productionOrderId);
                    cmd.Parameters.AddWithValue("@PO_RouteStageId", id);
                    cmd.Parameters.AddWithValue("@PO_RouteStage", routeStages.PO_RouteStage);
                    cmd.Parameters.AddWithValue("@PO_Type", routeStages.PO_Type);
                    cmd.Parameters.AddWithValue("@PO_ItemNo", routeStages.PO_ItemNo);
                    cmd.Parameters.AddWithValue("@PO_ItemDescription", routeStages.PO_ItemDescription);
                    cmd.Parameters.AddWithValue("@PO_Quantity", routeStages.PO_Quantity);
                    cmd.Parameters.AddWithValue("@PO_Uom", routeStages.PO_Uom);
                    cmd.Parameters.AddWithValue("@PO_WarehouseId", routeStages.PO_WarehouseId);
                    cmd.Parameters.AddWithValue("@PO_UnitPrice", routeStages.PO_UnitPrice);
                    cmd.Parameters.AddWithValue("@PO_Total", routeStages.PO_Total);
                    cmd.Parameters.AddWithValue("@PO_Status", routeStages.PO_Status);


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

            return routeStages;
        }
    }
}
