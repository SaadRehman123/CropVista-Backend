using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class PurchaseRequestServices
    {
        public string AddPurchaseRequest(SqlConnection connection, PurchaseRequest purchaseRequest)
        {
            string purchaseRequestId = "";

            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreatePurchaseRequest", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@PR_CreationDate", purchaseRequest.PR_CreationDate);
                        cmd.Parameters.AddWithValue("@PR_RequiredBy", purchaseRequest.PR_RequiredBy);
                        cmd.Parameters.AddWithValue("@PR_Status", purchaseRequest.PR_Status);

                        // Output parameter to capture the generated ID
                        SqlParameter outputParam = new SqlParameter("@purchaseRequestId", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        purchaseRequestId = outputParam.Value.ToString();
                    }

                    foreach (var item in purchaseRequest.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreatePurchaseRequestItem", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 1);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@purchaseRequestId", purchaseRequestId);

                            SqlParameter outputParam = new SqlParameter("@PR_itemId", SqlDbType.NVarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            item.PR_itemId = outputParam.Value.ToString();
                        }
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                // Optionally log the exception
                throw new Exception("An error occurred while adding the purchase request: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return purchaseRequestId;
        }
        public void AddPurchaseRequestItems(SqlConnection connection, List<PurchaseRequestItems> purchaseRequestItems, string purchaseRequestId)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreatePurchaseRequestItem", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    foreach (var item in purchaseRequestItems)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@itemId", item.itemId);
                        cmd.Parameters.AddWithValue("@itemName", item.itemName);
                        cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                        cmd.Parameters.AddWithValue("@uom", item.uom);
                        cmd.Parameters.AddWithValue("@purchaseRequestId", purchaseRequestId);

                        SqlParameter outputParam = new SqlParameter("@PR_itemId", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        item.PR_itemId = outputParam.Value.ToString();
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
        public void DeletePurchaseRequestItem(SqlConnection connection, List<PurchaseRequestItems> purchaseRequestItems)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    foreach (var item in purchaseRequestItems)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreatePurchaseRequestItem", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 3);
                            cmd.Parameters.AddWithValue("@PR_itemId", item.PR_itemId);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@purchaseRequestId", item.PR_Id);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the purchase request: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public void UpdatePurchaseRequest(SqlConnection connection, PurchaseRequest purchaseRequest, string PR_Id)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreatePurchaseRequest", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 2);
                        cmd.Parameters.AddWithValue("@purchaseRequestId", PR_Id);
                        cmd.Parameters.AddWithValue("@PR_CreationDate", purchaseRequest.PR_CreationDate);
                        cmd.Parameters.AddWithValue("@PR_RequiredBy", purchaseRequest.PR_RequiredBy);
                        cmd.Parameters.AddWithValue("@PR_Status", purchaseRequest.PR_Status);

                        cmd.ExecuteNonQuery();
                    }

                    foreach (var item in purchaseRequest.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreatePurchaseRequestItem", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 2);
                            cmd.Parameters.AddWithValue("@PR_itemId", item.PR_itemId);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@purchaseRequestId", PR_Id);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the purchase request: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        public List<PurchaseRequest> GetPurchaseRequest(SqlConnection connection, string PR_Id)
        {
            List<PurchaseRequest> resultList = new List<PurchaseRequest>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreatePurchaseRequest", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@purchaseRequestId", PR_Id);
                    command.Parameters.AddWithValue("@PR_CreationDate", "");
                    command.Parameters.AddWithValue("@PR_RequiredBy", "");
                    command.Parameters.AddWithValue("@PR_Status", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, PurchaseRequest> purchaseRequestDictionary = new Dictionary<string, PurchaseRequest>();

                        while (reader.Read())
                        {
                            string purchaseRequestId = reader.GetString(reader.GetOrdinal("purchaseRequestId"));

                            if (!purchaseRequestDictionary.ContainsKey(purchaseRequestId))
                            {
                                PurchaseRequest purchase = new PurchaseRequest
                                {
                                    purchaseRequestId = purchaseRequestId,
                                    PR_CreationDate = reader.GetDateTime(reader.GetOrdinal("PR_CreationDate")).ToString("yyyy-MM-dd"),
                                    PR_RequiredBy = reader.GetDateTime(reader.GetOrdinal("PR_RequiredBy")).ToString("yyyy-MM-dd"),
                                    PR_Status = reader.GetString(reader.GetOrdinal("PR_Status")),
                                    Children = new List<PurchaseRequestItems>()
                                };

                                purchaseRequestDictionary.Add(purchaseRequestId, purchase);
                            }

                            PurchaseRequestItems purchaseRequest = new PurchaseRequestItems
                            {
                                PR_itemId = reader.GetString(reader.GetOrdinal("PR_itemId")),
                                itemId = reader.GetString(reader.GetOrdinal("itemId")),
                                itemName = reader.GetString(reader.GetOrdinal("itemName")),
                                itemQuantity = reader.GetInt32(reader.GetOrdinal("itemQuantity")),
                                uom = reader.GetString(reader.GetOrdinal("uom")),
                                PR_Id = reader.GetString(reader.GetOrdinal("PR_Id"))
                            };

                            purchaseRequestDictionary[purchaseRequestId].Children.Add(purchaseRequest);
                        }

                        resultList.AddRange(purchaseRequestDictionary.Values);
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