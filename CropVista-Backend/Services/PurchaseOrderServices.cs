using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class PurchaseOrderServices
    {
        public string AddPurchaseOrder(SqlConnection connection, PurchaseOrder purchaseOrder)
        {
            string pro_Id = "";

            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreatePurchaseOrder", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@creationDate", purchaseOrder.creationDate);
                        cmd.Parameters.AddWithValue("@requiredBy", purchaseOrder.requiredBy);
                        cmd.Parameters.AddWithValue("@pr_Id", purchaseOrder.pr_Id);
                        cmd.Parameters.AddWithValue("@vendorId", purchaseOrder.vendorId);
                        cmd.Parameters.AddWithValue("@vendorName", purchaseOrder.vendorName);
                        cmd.Parameters.AddWithValue("@vendorAddress", purchaseOrder.vendorAddress);
                        cmd.Parameters.AddWithValue("@vendorNumber", purchaseOrder.vendorNumber);
                        cmd.Parameters.AddWithValue("@purchaseOrderStatus", purchaseOrder.purchaseOrderStatus);
                        cmd.Parameters.AddWithValue("@total", purchaseOrder.total);

                        // Output parameter to capture the generated ID
                        SqlParameter outputParam = new SqlParameter("@pro_Id", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        pro_Id = outputParam.Value.ToString();
                    }

                    foreach (var item in purchaseOrder.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreatePurchaseOrderItems", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 1);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@pro_Id", pro_Id);

                            SqlParameter outputParam = new SqlParameter("@pro_ItemId", SqlDbType.NVarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            item.pro_ItemId = outputParam.Value.ToString();
                        }
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while adding the purchase request: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return pro_Id;
        }

        public List<PurchaseOrder> GetPurchaseOrder(SqlConnection connection, string pro_Id)
        {
            List<PurchaseOrder> resultList = new List<PurchaseOrder>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreatePurchaseOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@pro_Id", pro_Id);
                    command.Parameters.AddWithValue("@creationDate", "");
                    command.Parameters.AddWithValue("@requiredBy", "");
                    command.Parameters.AddWithValue("@pr_Id", "");
                    command.Parameters.AddWithValue("@vendorId", "");
                    command.Parameters.AddWithValue("@vendorName", "");
                    command.Parameters.AddWithValue("@vendorAddress", "");
                    command.Parameters.AddWithValue("@vendorNumber", "");
                    command.Parameters.AddWithValue("@total", "");
                    command.Parameters.AddWithValue("@purchaseOrderStatus", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, PurchaseOrder> purchaseOrderDictionary = new Dictionary<string, PurchaseOrder>();

                        while (reader.Read())
                        {
                            string purchaseOrderId = reader.GetString(reader.GetOrdinal("pro_Id"));

                            if (!purchaseOrderDictionary.ContainsKey(purchaseOrderId))
                            {
                                PurchaseOrder purchaseOrder = new PurchaseOrder
                                {
                                    pro_Id = purchaseOrderId,
                                    creationDate = reader.GetDateTime(reader.GetOrdinal("creationDate")).ToString("yyyy-MM-dd"),
                                    requiredBy = reader.GetDateTime(reader.GetOrdinal("requiredBy")).ToString("yyyy-MM-dd"),
                                    pr_Id = reader.GetString(reader.GetOrdinal("pr_Id")),
                                    vendorId = reader.GetString(reader.GetOrdinal("vendorId")),
                                    vendorName = reader.GetString(reader.GetOrdinal("vendorName")),
                                    vendorAddress = reader.GetString(reader.GetOrdinal("vendorAddress")),
                                    vendorNumber = reader.GetString(reader.GetOrdinal("vendorNumber")),
                                    total = reader.GetInt32(reader.GetOrdinal("total")),
                                    purchaseOrderStatus = reader.GetString(reader.GetOrdinal("purchaseOrderStatus")),
                                    Children = new List<PurchaseOrderItems>()
                                };

                                purchaseOrderDictionary.Add(purchaseOrderId, purchaseOrder);
                            }

                            PurchaseOrderItems purchaseOrderItems = new PurchaseOrderItems
                            {
                                pro_ItemId = reader.GetString(reader.GetOrdinal("pro_ItemId")),
                                itemId = reader.GetString(reader.GetOrdinal("itemId")),
                                itemName = reader.GetString(reader.GetOrdinal("itemName")),
                                itemQuantity = reader.GetInt32(reader.GetOrdinal("itemQuantity")),
                                uom = reader.GetString(reader.GetOrdinal("uom")),
                                rate = reader.GetInt32(reader.GetOrdinal("rate")),
                                amount = reader.GetInt32(reader.GetOrdinal("amount")),
                                pro_Id = reader.GetString(reader.GetOrdinal("pro_Id"))
                            };

                            purchaseOrderDictionary[purchaseOrderId].Children.Add(purchaseOrderItems);
                        }

                        resultList.AddRange(purchaseOrderDictionary.Values);
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
