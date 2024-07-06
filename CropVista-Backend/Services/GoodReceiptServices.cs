using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class GoodReceiptServices
    {
        public string AddGoodReceipt(SqlConnection connection, GoodReceipt goodReceipt)
        {
            string gr_Id = "";

            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateGoodReceipt", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@pro_Id", goodReceipt.pro_Id);
                        cmd.Parameters.AddWithValue("@vendorId", goodReceipt.vendorId);
                        cmd.Parameters.AddWithValue("@vendorName", goodReceipt.vendorName);
                        cmd.Parameters.AddWithValue("@vendorAddress", goodReceipt.vendorAddress);
                        cmd.Parameters.AddWithValue("@vendorNumber", goodReceipt.vendorNumber);
                        cmd.Parameters.AddWithValue("@creationDate", goodReceipt.creationDate);
                        cmd.Parameters.AddWithValue("@total", goodReceipt.total);
                        cmd.Parameters.AddWithValue("@gr_Status", goodReceipt.gr_Status);

                        // Output parameter to capture the generated ID
                        SqlParameter outputParam = new SqlParameter("@gr_Id", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        gr_Id = outputParam.Value.ToString();
                    }

                    foreach (var item in goodReceipt.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateGoodReceiptItems", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 1);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@gr_Id", gr_Id);

                            SqlParameter outputParam = new SqlParameter("@gr_ItemId", SqlDbType.NVarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            item.gr_ItemId = outputParam.Value.ToString();
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

            return gr_Id;
        }
        public void UpdateGoodReceipt(SqlConnection connection, GoodReceipt goodReceipt, string gr_Id)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateGoodReceipt", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 2);
                        cmd.Parameters.AddWithValue("@gr_Id", gr_Id);
                        cmd.Parameters.AddWithValue("@pro_Id", goodReceipt.pro_Id);
                        cmd.Parameters.AddWithValue("@vendorId", goodReceipt.vendorId);
                        cmd.Parameters.AddWithValue("@vendorName", goodReceipt.vendorName);
                        cmd.Parameters.AddWithValue("@vendorAddress", goodReceipt.vendorAddress);
                        cmd.Parameters.AddWithValue("@vendorNumber", goodReceipt.vendorNumber);
                        cmd.Parameters.AddWithValue("@creationDate", goodReceipt.creationDate);
                        cmd.Parameters.AddWithValue("@total", goodReceipt.total);
                        cmd.Parameters.AddWithValue("@gr_Status", goodReceipt.gr_Status);

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
        public List<GoodReceipt> GetGoodReceipt(SqlConnection connection, string gr_Id)
        {
            List<GoodReceipt> resultList = new List<GoodReceipt>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateGoodReceipt", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@gr_Id", gr_Id);
                    command.Parameters.AddWithValue("@pro_Id", "");
                    command.Parameters.AddWithValue("@vendorId", "");
                    command.Parameters.AddWithValue("@vendorName", "");
                    command.Parameters.AddWithValue("@vendorAddress", "");
                    command.Parameters.AddWithValue("@vendorNumber", "");
                    command.Parameters.AddWithValue("@creationDate", "");
                    command.Parameters.AddWithValue("@total", "");
                    command.Parameters.AddWithValue("@gr_Status", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, GoodReceipt> goodReceiptDictionary = new Dictionary<string, GoodReceipt>();

                        while (reader.Read())
                        {
                            string goodReceiptId = reader.GetString(reader.GetOrdinal("gr_Id"));

                            if (!goodReceiptDictionary.ContainsKey(goodReceiptId))
                            {
                                GoodReceipt goodReceipt = new GoodReceipt
                                {
                                    gr_Id = goodReceiptId,
                                    pro_Id = reader.GetString(reader.GetOrdinal("pro_Id")),
                                    vendorId = reader.GetString(reader.GetOrdinal("vendorId")),
                                    vendorName = reader.GetString(reader.GetOrdinal("vendorName")),
                                    vendorAddress = reader.GetString(reader.GetOrdinal("vendorAddress")),
                                    vendorNumber = reader.GetString(reader.GetOrdinal("vendorNumber")),
                                    creationDate = reader.GetDateTime(reader.GetOrdinal("creationDate")).ToString("yyyy-MM-dd"),
                                    total = reader.GetInt32(reader.GetOrdinal("total")),
                                    gr_Status = reader.GetString(reader.GetOrdinal("gr_Status")),
                                    Children = new List<GoodReceiptItems>()
                                };

                                goodReceiptDictionary.Add(goodReceiptId, goodReceipt);
                            }

                            GoodReceiptItems goodReceiptItems = new GoodReceiptItems
                            {
                                gr_ItemId = reader.GetString(reader.GetOrdinal("gr_ItemId")),
                                itemId = reader.GetString(reader.GetOrdinal("itemId")),
                                itemName = reader.GetString(reader.GetOrdinal("itemName")),
                                itemQuantity = reader.GetInt32(reader.GetOrdinal("itemQuantity")),
                                uom = reader.GetString(reader.GetOrdinal("uom")),
                                rate = reader.GetInt32(reader.GetOrdinal("rate")),
                                amount = reader.GetInt32(reader.GetOrdinal("amount")),
                                gr_Id = reader.GetString(reader.GetOrdinal("gr_Id"))
                            };

                            goodReceiptDictionary[goodReceiptId].Children.Add(goodReceiptItems);
                        }

                        resultList.AddRange(goodReceiptDictionary.Values);
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
