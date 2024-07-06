using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class PurchaseInvoiceServices
    {
        public string AddPurchaseInvoice(SqlConnection connection, PurchaseInvoice purchaseInvoice)
        {
            string pi_Id = "";

            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreatePurchaseInvoice", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@dueDate", purchaseInvoice.dueDate);
                        cmd.Parameters.AddWithValue("@creationDate", purchaseInvoice.creationDate);
                        cmd.Parameters.AddWithValue("@gr_Id", purchaseInvoice.gr_Id);
                        cmd.Parameters.AddWithValue("@vendorId", purchaseInvoice.vendorId);
                        cmd.Parameters.AddWithValue("@vendorName", purchaseInvoice.vendorName);
                        cmd.Parameters.AddWithValue("@vendorAddress", purchaseInvoice.vendorAddress);
                        cmd.Parameters.AddWithValue("@vendorNumber", purchaseInvoice.vendorNumber);
                        cmd.Parameters.AddWithValue("@pi_Status", purchaseInvoice.pi_Status);
                        cmd.Parameters.AddWithValue("@paid", purchaseInvoice.paid);
                        cmd.Parameters.AddWithValue("@total", purchaseInvoice.total);

                        // Output parameter to capture the generated ID
                        SqlParameter outputParam = new SqlParameter("@pi_Id", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        pi_Id = outputParam.Value.ToString();
                    }

                    foreach (var item in purchaseInvoice.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreatePurchaseInvoiceItems", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 1);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@pi_Id", pi_Id);

                            SqlParameter outputParam = new SqlParameter("@pi_ItemId", SqlDbType.NVarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            item.pi_ItemId = outputParam.Value.ToString();
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

            return pi_Id;
        }
        public void UpdatePurchaseInvoice(SqlConnection connection, PurchaseInvoice purchaseInvoice, string pi_Id)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreatePurchaseInvoice", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 2);
                        cmd.Parameters.AddWithValue("@pi_Id", pi_Id);
                        cmd.Parameters.AddWithValue("@dueDate", purchaseInvoice.dueDate);
                        cmd.Parameters.AddWithValue("@creationDate", purchaseInvoice.creationDate);
                        cmd.Parameters.AddWithValue("@gr_Id", purchaseInvoice.gr_Id);
                        cmd.Parameters.AddWithValue("@vendorId", purchaseInvoice.vendorId);
                        cmd.Parameters.AddWithValue("@vendorName", purchaseInvoice.vendorName);
                        cmd.Parameters.AddWithValue("@vendorAddress", purchaseInvoice.vendorAddress);
                        cmd.Parameters.AddWithValue("@vendorNumber", purchaseInvoice.vendorNumber);
                        cmd.Parameters.AddWithValue("@pi_Status", purchaseInvoice.pi_Status);
                        cmd.Parameters.AddWithValue("@paid", purchaseInvoice.paid);
                        cmd.Parameters.AddWithValue("@total", purchaseInvoice.total);

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
        public List<PurchaseInvoice> GetPurchaseInvoice(SqlConnection connection, string pi_Id)
        {
            List<PurchaseInvoice> resultList = new List<PurchaseInvoice>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreatePurchaseInvoice", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@pi_Id", pi_Id);
                    command.Parameters.AddWithValue("@dueDate", "");
                    command.Parameters.AddWithValue("@creationDate", "");
                    command.Parameters.AddWithValue("@gr_Id", "");
                    command.Parameters.AddWithValue("@vendorId", "");
                    command.Parameters.AddWithValue("@vendorName", "");
                    command.Parameters.AddWithValue("@vendorAddress", "");
                    command.Parameters.AddWithValue("@vendorNumber", "");
                    command.Parameters.AddWithValue("@pi_Status", "");
                    command.Parameters.AddWithValue("@paid", "");
                    command.Parameters.AddWithValue("@total", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, PurchaseInvoice> purchaseInvoiceDictionary = new Dictionary<string, PurchaseInvoice>();

                        while (reader.Read())
                        {
                            string purchaseInvoiceId = reader.GetString(reader.GetOrdinal("pi_Id"));

                            if (!purchaseInvoiceDictionary.ContainsKey(purchaseInvoiceId))
                            {
                                PurchaseInvoice purchaseInvoice = new PurchaseInvoice
                                {
                                    pi_Id = purchaseInvoiceId,
                                    dueDate = reader.GetDateTime(reader.GetOrdinal("dueDate")).ToString("yyyy-MM-dd"),
                                    creationDate = reader.GetDateTime(reader.GetOrdinal("creationDate")).ToString("yyyy-MM-dd"),
                                    gr_Id = reader.GetString(reader.GetOrdinal("gr_Id")),
                                    vendorId = reader.GetString(reader.GetOrdinal("vendorId")),
                                    vendorName = reader.GetString(reader.GetOrdinal("vendorName")),
                                    vendorAddress = reader.GetString(reader.GetOrdinal("vendorAddress")),
                                    vendorNumber = reader.GetString(reader.GetOrdinal("vendorNumber")),
                                    pi_Status = reader.GetString(reader.GetOrdinal("pi_Status")),
                                    paid = reader.GetBoolean(reader.GetOrdinal("paid")),
                                    total = reader.GetInt32(reader.GetOrdinal("total")),
                                    Children = new List<PurchaseInvoiceItems>()
                                };

                                purchaseInvoiceDictionary.Add(purchaseInvoiceId, purchaseInvoice);
                            }

                            PurchaseInvoiceItems purchaseInvoiceItems = new PurchaseInvoiceItems
                            {
                                pi_ItemId = reader.GetString(reader.GetOrdinal("pi_ItemId")),
                                itemId = reader.GetString(reader.GetOrdinal("itemId")),
                                itemName = reader.GetString(reader.GetOrdinal("itemName")),
                                itemQuantity = reader.GetInt32(reader.GetOrdinal("itemQuantity")),
                                uom = reader.GetString(reader.GetOrdinal("uom")),
                                rate = reader.GetInt32(reader.GetOrdinal("rate")),
                                amount = reader.GetInt32(reader.GetOrdinal("amount")),
                                pi_Id = reader.GetString(reader.GetOrdinal("pi_Id"))
                            };

                            purchaseInvoiceDictionary[purchaseInvoiceId].Children.Add(purchaseInvoiceItems);
                        }

                        resultList.AddRange(purchaseInvoiceDictionary.Values);
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
