using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class ProductionOrderServices
    {
        public string AddProductionOrder(SqlConnection connection, ProductionOrder productionOrder)
        {
            string productionOrderId = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateProductionOrder", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 1);
                    cmd.Parameters.AddWithValue("@productionNo", productionOrder.productionNo);
                    cmd.Parameters.AddWithValue("@productDescription", productionOrder.productDescription);
                    cmd.Parameters.AddWithValue("@productionStdCost", productionOrder.productionStdCost);
                    cmd.Parameters.AddWithValue("@quantity", productionOrder.quantity);
                    cmd.Parameters.AddWithValue("@status", productionOrder.status);
                    cmd.Parameters.AddWithValue("@currentDate", productionOrder.currentDate);
                    cmd.Parameters.AddWithValue("@startDate", productionOrder.startDate);
                    cmd.Parameters.AddWithValue("@endDate", productionOrder.endDate);
                    cmd.Parameters.AddWithValue("@warehouse", productionOrder.warehouse);
                    cmd.Parameters.AddWithValue("@productionId", "");

                    // Output parameter to capture the generated ID
                    SqlParameter outputParam = new SqlParameter("@productionOrderId", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    productionOrderId = outputParam.Value.ToString();
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

            return productionOrderId;
        }
        public ProductionOrder UpdateProductionOrder(SqlConnection connection, ProductionOrder productionOrder, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateProductionOrder", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 2);
                    cmd.Parameters.AddWithValue("@productionOrderId", id);
                    cmd.Parameters.AddWithValue("@productionNo", productionOrder.productionNo);
                    cmd.Parameters.AddWithValue("@productDescription", productionOrder.productDescription);
                    cmd.Parameters.AddWithValue("@productionStdCost", productionOrder.productionStdCost);
                    cmd.Parameters.AddWithValue("@quantity", productionOrder.quantity);
                    cmd.Parameters.AddWithValue("@status", productionOrder.status);
                    cmd.Parameters.AddWithValue("@currentDate", productionOrder.currentDate);
                    cmd.Parameters.AddWithValue("@startDate", productionOrder.startDate);
                    cmd.Parameters.AddWithValue("@endDate", productionOrder.endDate);
                    cmd.Parameters.AddWithValue("@warehouse", productionOrder.warehouse);
                    cmd.Parameters.AddWithValue("@productionId", "");

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

            return productionOrder;
        }
        public ProductionOrder DeleteProductionOrder(SqlConnection connection, ProductionOrder productionOrder, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateProductionOrder", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 3);
                    cmd.Parameters.AddWithValue("@productionOrderId", id);
                    cmd.Parameters.AddWithValue("@productionNo", productionOrder.productionNo);
                    cmd.Parameters.AddWithValue("@productDescription", productionOrder.productDescription);
                    cmd.Parameters.AddWithValue("@productionStdCost", productionOrder.productionStdCost);
                    cmd.Parameters.AddWithValue("@quantity", productionOrder.quantity);
                    cmd.Parameters.AddWithValue("@status", productionOrder.status);
                    cmd.Parameters.AddWithValue("@currentDate", productionOrder.currentDate);
                    cmd.Parameters.AddWithValue("@startDate", productionOrder.startDate);
                    cmd.Parameters.AddWithValue("@endDate", productionOrder.endDate);
                    cmd.Parameters.AddWithValue("@warehouse", productionOrder.warehouse);
                    cmd.Parameters.AddWithValue("@productionId", "");

                    connection.Open();
                    int i = cmd.ExecuteNonQuery();
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

            return productionOrder;
        }
        public List<ProductionOrder> GetProductionOrder(SqlConnection connection, string productionId)
        {
            List<ProductionOrder> resultList = new List<ProductionOrder>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateProductionOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@productionOrderId", "");
                    command.Parameters.AddWithValue("@productionNo", "");
                    command.Parameters.AddWithValue("@productDescription", "");
                    command.Parameters.AddWithValue("@productionStdCost", "");
                    command.Parameters.AddWithValue("@quantity", "");
                    command.Parameters.AddWithValue("@status", "");
                    command.Parameters.AddWithValue("@currentDate", "");
                    command.Parameters.AddWithValue("@startDate", "");
                    command.Parameters.AddWithValue("@endDate", "");
                    command.Parameters.AddWithValue("@warehouse", "");
                    command.Parameters.AddWithValue("@productionId", productionId);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, ProductionOrder> productionOrderDictionary = new Dictionary<string, ProductionOrder>();

                        while (reader.Read())
                        {
                            string productionOrderId = reader.GetString(reader.GetOrdinal("productionOrderId"));

                            if (!productionOrderDictionary.ContainsKey(productionOrderId))
                            {
                                ProductionOrder productionOrder = new ProductionOrder
                                {
                                    productionOrderId = productionOrderId,
                                    productionNo = reader.GetString(reader.GetOrdinal("productionNo")),
                                    productDescription = reader.GetString(reader.GetOrdinal("productDescription")),
                                    productionStdCost = (float)reader.GetDouble(reader.GetOrdinal("productionStdCost")),
                                    quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                    status = reader.GetString(reader.GetOrdinal("status")),
                                    currentDate = reader.GetDateTime(reader.GetOrdinal("currentDate")).ToString("yyyy-MM-dd"),
                                    startDate = reader.GetDateTime(reader.GetOrdinal("startDate")).ToString("yyyy-MM-dd"),
                                    endDate = reader.GetDateTime(reader.GetOrdinal("endDate")).ToString("yyyy-MM-dd"),
                                    warehouse = reader.GetString(reader.GetOrdinal("warehouse")),
                                    Children = new List<PO_RouteStages>()
                                };

                                productionOrderDictionary.Add(productionOrderId, productionOrder);
                            }

                            PO_RouteStages routeStages = new PO_RouteStages
                            {
                                PO_productionOrderId = reader.GetString(reader.GetOrdinal("PO_productionOrderId")),
                                PO_RouteStageId = reader.GetString(reader.GetOrdinal("PO_RouteStageId")),
                                PO_RouteStage = reader.GetInt32(reader.GetOrdinal("PO_RouteStage")),
                                PO_Type = reader.GetString(reader.GetOrdinal("PO_Type")),
                                PO_ItemNo = reader.GetString(reader.GetOrdinal("PO_ItemNo")),
                                PO_ItemDescription = reader.GetString(reader.GetOrdinal("PO_ItemDescription")),
                                PO_Quantity = reader.GetInt32(reader.GetOrdinal("PO_Quantity")),
                                PO_Uom = reader.GetString(reader.GetOrdinal("PO_Uom")),
                                PO_WarehouseId = reader.GetString(reader.GetOrdinal("PO_WarehouseId")),
                                PO_UnitPrice = (float)reader.GetDouble(reader.GetOrdinal("PO_UnitPrice")),
                                PO_Total = (float)reader.GetDouble(reader.GetOrdinal("PO_Total")),
                                PO_Status = reader.GetString(reader.GetOrdinal("PO_Status"))
                            };

                            productionOrderDictionary[productionOrderId].Children.Add(routeStages);
                        }

                        resultList.AddRange(productionOrderDictionary.Values);
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

            return resultList;
        }
    }
}
