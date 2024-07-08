using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class SaleOrderServices
    {
        public string AddSaleOrder(SqlConnection connection, SaleOrder saleOrder)
        {
            string saleOrderId = "";

            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateSaleOrder", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@creationDate", saleOrder.creationDate);
                        cmd.Parameters.AddWithValue("@deliveryDate", saleOrder.deliveryDate);
                        cmd.Parameters.AddWithValue("@customerId", saleOrder.customerId);
                        cmd.Parameters.AddWithValue("@customerName", saleOrder.customerName);
                        cmd.Parameters.AddWithValue("@customerAddress", saleOrder.customerAddress);
                        cmd.Parameters.AddWithValue("@customerNumber", saleOrder.customerNumber);
                        cmd.Parameters.AddWithValue("@total", saleOrder.total);
                        cmd.Parameters.AddWithValue("@so_Status", saleOrder.so_Status);

                        // Output parameter to capture the generated ID
                        SqlParameter outputParam = new SqlParameter("@saleOrder_Id", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        saleOrderId = outputParam.Value.ToString();
                    }

                    foreach (var item in saleOrder.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateSaleOrderItems", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 1);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@saleOrder_Id", saleOrderId);

                            SqlParameter outputParam = new SqlParameter("@so_ItemId", SqlDbType.NVarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            item.so_ItemId = outputParam.Value.ToString();
                        }
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                throw new Exception(ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return saleOrderId;
        }
        public void AddSaleOrderItems(SqlConnection connection, List<SaleOrderItems> saleOrderItems, string so_Id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateSaleOrderItems", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    foreach (var item in saleOrderItems)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@itemId", item.itemId);
                        cmd.Parameters.AddWithValue("@itemName", item.itemName);
                        cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                        cmd.Parameters.AddWithValue("@uom", item.uom);
                        cmd.Parameters.AddWithValue("@rate", item.rate);
                        cmd.Parameters.AddWithValue("@amount", item.amount);
                        cmd.Parameters.AddWithValue("@saleOrder_Id", so_Id);

                        SqlParameter outputParam = new SqlParameter("@so_ItemId", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        item.so_ItemId = outputParam.Value.ToString();
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
        public void DeleteSaleOrderItems(SqlConnection connection, List<SaleOrderItems> saleOrderItems)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    foreach (var item in saleOrderItems)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateSaleOrderItems", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 3);
                            cmd.Parameters.AddWithValue("@so_ItemId", item.so_ItemId);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@saleOrder_Id", item.saleOrder_Id);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
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
        public void UpdateSaleOrder(SqlConnection connection, SaleOrder saleOrder, string so_Id)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateSaleOrder", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 2);
                        cmd.Parameters.AddWithValue("@saleOrder_Id", so_Id);
                        cmd.Parameters.AddWithValue("@creationDate", saleOrder.creationDate);
                        cmd.Parameters.AddWithValue("@deliveryDate", saleOrder.deliveryDate);
                        cmd.Parameters.AddWithValue("@customerId", saleOrder.customerId);
                        cmd.Parameters.AddWithValue("@customerName", saleOrder.customerName);
                        cmd.Parameters.AddWithValue("@customerAddress", saleOrder.customerAddress);
                        cmd.Parameters.AddWithValue("@customerNumber", saleOrder.customerNumber);
                        cmd.Parameters.AddWithValue("@total", saleOrder.total);
                        cmd.Parameters.AddWithValue("@so_Status", saleOrder.so_Status);

                        cmd.ExecuteNonQuery();
                    }

                    foreach (var item in saleOrder.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateSaleOrderItems", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 2);
                            cmd.Parameters.AddWithValue("@so_ItemId", item.so_ItemId);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@saleOrder_Id", so_Id);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
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
        public List<SaleOrder> GetSaleOrder(SqlConnection connection, string so_Id)
        {
            List<SaleOrder> resultList = new List<SaleOrder>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateSaleOrder", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@saleOrder_Id", so_Id);
                    command.Parameters.AddWithValue("@creationDate", "");
                    command.Parameters.AddWithValue("@deliveryDate", "");
                    command.Parameters.AddWithValue("@customerId", "");
                    command.Parameters.AddWithValue("@customerName", "");
                    command.Parameters.AddWithValue("@customerAddress", "");
                    command.Parameters.AddWithValue("@customerNumber", "");
                    command.Parameters.AddWithValue("@total", "");
                    command.Parameters.AddWithValue("@so_Status", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, SaleOrder> saleOrderDictionary = new Dictionary<string, SaleOrder>();

                        while (reader.Read())
                        {
                            string saleOrderId = reader.GetString(reader.GetOrdinal("saleOrder_Id"));

                            if (!saleOrderDictionary.ContainsKey(saleOrderId))
                            {
                                SaleOrder sale = new SaleOrder
                                {
                                    saleOrder_Id = saleOrderId,
                                    creationDate = reader.GetDateTime(reader.GetOrdinal("creationDate")).ToString("yyyy-MM-dd"),
                                    deliveryDate = reader.GetDateTime(reader.GetOrdinal("deliveryDate")).ToString("yyyy-MM-dd"),
                                    customerId = reader.GetString(reader.GetOrdinal("customerId")),
                                    customerName = reader.GetString(reader.GetOrdinal("customerName")),
                                    customerAddress = reader.GetString(reader.GetOrdinal("customerAddress")),
                                    customerNumber = reader.GetString(reader.GetOrdinal("customerNumber")),
                                    total = reader.GetInt32(reader.GetOrdinal("total")),
                                    so_Status = reader.GetString(reader.GetOrdinal("so_Status")),
                                    Children = new List<SaleOrderItems>()
                                };

                                saleOrderDictionary.Add(saleOrderId, sale);
                            }

                            SaleOrderItems saleOrderItems = new SaleOrderItems
                            {
                                so_ItemId = reader.GetString(reader.GetOrdinal("so_ItemId")),
                                itemId = reader.GetString(reader.GetOrdinal("itemId")),
                                itemName = reader.GetString(reader.GetOrdinal("itemName")),
                                itemQuantity = reader.GetInt32(reader.GetOrdinal("itemQuantity")),
                                uom = reader.GetString(reader.GetOrdinal("uom")),
                                rate = reader.GetInt32(reader.GetOrdinal("rate")),
                                amount = reader.GetInt32(reader.GetOrdinal("amount")),
                                saleOrder_Id = reader.GetString(reader.GetOrdinal("saleOrder_Id"))
                            };

                            saleOrderDictionary[saleOrderId].Children.Add(saleOrderItems);
                        }

                        resultList.AddRange(saleOrderDictionary.Values);
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
