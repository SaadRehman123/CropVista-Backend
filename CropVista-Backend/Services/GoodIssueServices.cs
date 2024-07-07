using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class GoodIssueServices
    {
        public string AddGoodIssue(SqlConnection connection, GoodIssue goodIssue)
        {
            string gi_Id = "";

            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateGoodIssue", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@saleOrder_Id", goodIssue.saleOrder_Id);
                        cmd.Parameters.AddWithValue("@creationDate", goodIssue.creationDate);
                        cmd.Parameters.AddWithValue("@customerId", goodIssue.customerId);
                        cmd.Parameters.AddWithValue("@customerName", goodIssue.customerName);
                        cmd.Parameters.AddWithValue("@customerAddress", goodIssue.customerAddress);
                        cmd.Parameters.AddWithValue("@customerNumber", goodIssue.customerNumber);
                        cmd.Parameters.AddWithValue("@total", goodIssue.total);
                        cmd.Parameters.AddWithValue("@gi_Status", goodIssue.gi_Status);

                        // Output parameter to capture the generated ID
                        SqlParameter outputParam = new SqlParameter("@gi_Id", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        gi_Id = outputParam.Value.ToString();
                    }

                    foreach (var item in goodIssue.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateGoodIssueItems", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 1);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@gi_Id", gi_Id);

                            SqlParameter outputParam = new SqlParameter("@gi_ItemId", SqlDbType.NVarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            item.gi_ItemId = outputParam.Value.ToString();
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

            return gi_Id;
        }
        public void UpdateGoodIssue(SqlConnection connection, GoodIssue goodIssue, string gi_Id)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateGoodIssue", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 2);
                        cmd.Parameters.AddWithValue("@gi_Id", gi_Id);
                        cmd.Parameters.AddWithValue("@saleOrder_Id", goodIssue.saleOrder_Id);
                        cmd.Parameters.AddWithValue("@creationDate", goodIssue.creationDate);
                        cmd.Parameters.AddWithValue("@customerId", goodIssue.customerId);
                        cmd.Parameters.AddWithValue("@customerName", goodIssue.customerName);
                        cmd.Parameters.AddWithValue("@customerAddress", goodIssue.customerAddress);
                        cmd.Parameters.AddWithValue("@customerNumber", goodIssue.customerNumber);
                        cmd.Parameters.AddWithValue("@total", goodIssue.total);
                        cmd.Parameters.AddWithValue("@gi_Status", goodIssue.gi_Status);

                        cmd.ExecuteNonQuery();
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
        public List<GoodIssue> GetGoodIssue(SqlConnection connection, string gi_Id)
        {
            List<GoodIssue> resultList = new List<GoodIssue>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateGoodIssue", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@gi_Id", gi_Id);
                    command.Parameters.AddWithValue("@saleOrder_Id", "");
                    command.Parameters.AddWithValue("@creationDate", "");
                    command.Parameters.AddWithValue("@customerId", "");
                    command.Parameters.AddWithValue("@customerName", "");
                    command.Parameters.AddWithValue("@customerAddress", "");
                    command.Parameters.AddWithValue("@customerNumber", "");
                    command.Parameters.AddWithValue("@total", "");
                    command.Parameters.AddWithValue("@gi_Status", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, GoodIssue> goodIssueDictionary = new Dictionary<string, GoodIssue>();

                        while (reader.Read())
                        {
                            string goodIssueId = reader.GetString(reader.GetOrdinal("gi_Id"));

                            if (!goodIssueDictionary.ContainsKey(goodIssueId))
                            {
                                GoodIssue goodIssue = new GoodIssue
                                {
                                    gi_Id = goodIssueId,
                                    saleOrder_Id = reader.GetString(reader.GetOrdinal("saleOrder_Id")),
                                    creationDate = reader.GetDateTime(reader.GetOrdinal("creationDate")).ToString("yyyy-MM-dd"),
                                    customerId = reader.GetString(reader.GetOrdinal("customerId")),
                                    customerName = reader.GetString(reader.GetOrdinal("customerName")),
                                    customerAddress = reader.GetString(reader.GetOrdinal("customerAddress")),
                                    customerNumber = reader.GetString(reader.GetOrdinal("customerNumber")),
                                    total = reader.GetInt32(reader.GetOrdinal("total")),
                                    gi_Status = reader.GetString(reader.GetOrdinal("gi_Status")),
                                    Children = new List<GoodIssueItems>()
                                };

                                goodIssueDictionary.Add(goodIssueId, goodIssue);
                            }

                            GoodIssueItems goodIssueItems = new GoodIssueItems
                            {
                                gi_ItemId = reader.GetString(reader.GetOrdinal("gi_ItemId")),
                                itemId = reader.GetString(reader.GetOrdinal("itemId")),
                                itemName = reader.GetString(reader.GetOrdinal("itemName")),
                                itemQuantity = reader.GetInt32(reader.GetOrdinal("itemQuantity")),
                                uom = reader.GetString(reader.GetOrdinal("uom")),
                                rate = reader.GetInt32(reader.GetOrdinal("rate")),
                                amount = reader.GetInt32(reader.GetOrdinal("amount")),
                                gi_Id = reader.GetString(reader.GetOrdinal("gi_Id"))
                            };

                            goodIssueDictionary[goodIssueId].Children.Add(goodIssueItems);
                        }

                        resultList.AddRange(goodIssueDictionary.Values);
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
