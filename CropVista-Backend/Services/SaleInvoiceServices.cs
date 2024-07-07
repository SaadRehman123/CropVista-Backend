using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class SaleInvoiceServices
    {
        public string AddSaleInvoice(SqlConnection connection, SaleInvoice saleInvoice)
        {
            string si_Id = "";

            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateSalesInvoice", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@creationDate", saleInvoice.creationDate);
                        cmd.Parameters.AddWithValue("@dueDate", saleInvoice.dueDate);
                        cmd.Parameters.AddWithValue("@gi_Id", saleInvoice.gi_Id);
                        cmd.Parameters.AddWithValue("@customerId", saleInvoice.customerId);
                        cmd.Parameters.AddWithValue("@customerName", saleInvoice.customerName);
                        cmd.Parameters.AddWithValue("@customerAddress", saleInvoice.customerAddress);
                        cmd.Parameters.AddWithValue("@customerNumber", saleInvoice.customerNumber);
                        cmd.Parameters.AddWithValue("@total", saleInvoice.total);
                        cmd.Parameters.AddWithValue("@paid", saleInvoice.paid);
                        cmd.Parameters.AddWithValue("@si_Status", saleInvoice.si_Status);

                        // Output parameter to capture the generated ID
                        SqlParameter outputParam = new SqlParameter("@salesInvoice_Id", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        si_Id = outputParam.Value.ToString();
                    }

                    foreach (var item in saleInvoice.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateSalesInvoiceItems", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 1);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@salesInvoice_Id", si_Id);

                            SqlParameter outputParam = new SqlParameter("@si_ItemId", SqlDbType.NVarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            item.si_ItemId = outputParam.Value.ToString();
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

            return si_Id;
        }
        public void UpdateSaleInvoice(SqlConnection connection, SaleInvoice saleInvoice, string salesInvoice_Id)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateSalesInvoice", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 2);
                        cmd.Parameters.AddWithValue("@salesInvoice_Id", salesInvoice_Id);
                        cmd.Parameters.AddWithValue("@creationDate", saleInvoice.creationDate);
                        cmd.Parameters.AddWithValue("@dueDate", saleInvoice.dueDate);
                        cmd.Parameters.AddWithValue("@gi_Id", saleInvoice.gi_Id);
                        cmd.Parameters.AddWithValue("@customerId", saleInvoice.customerId);
                        cmd.Parameters.AddWithValue("@customerName", saleInvoice.customerName);
                        cmd.Parameters.AddWithValue("@customerAddress", saleInvoice.customerAddress);
                        cmd.Parameters.AddWithValue("@customerNumber", saleInvoice.customerNumber);
                        cmd.Parameters.AddWithValue("@total", saleInvoice.total);
                        cmd.Parameters.AddWithValue("@paid", saleInvoice.paid);
                        cmd.Parameters.AddWithValue("@si_Status", saleInvoice.si_Status);

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
        public List<SaleInvoice> GetSaleInvoice(SqlConnection connection, string salesInvoice_Id)
        {
            List<SaleInvoice> resultList = new List<SaleInvoice>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateSalesInvoice", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@salesInvoice_Id", salesInvoice_Id);
                    command.Parameters.AddWithValue("@creationDate", "");
                    command.Parameters.AddWithValue("@dueDate", "");
                    command.Parameters.AddWithValue("@gi_Id", "");
                    command.Parameters.AddWithValue("@customerId", "");
                    command.Parameters.AddWithValue("@customerName", "");
                    command.Parameters.AddWithValue("@customerAddress", "");
                    command.Parameters.AddWithValue("@customerNumber", "");
                    command.Parameters.AddWithValue("@total", "");
                    command.Parameters.AddWithValue("@paid", "");
                    command.Parameters.AddWithValue("@si_Status", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, SaleInvoice> saleInvoiceDictionary = new Dictionary<string, SaleInvoice>();

                        while (reader.Read())
                        {
                            string si_Id = reader.GetString(reader.GetOrdinal("salesInvoice_Id"));

                            if (!saleInvoiceDictionary.ContainsKey(si_Id))
                            {
                                SaleInvoice sale = new SaleInvoice
                                {
                                    salesInvoice_Id = si_Id,
                                    creationDate = reader.GetDateTime(reader.GetOrdinal("creationDate")).ToString("yyyy-MM-dd"),
                                    dueDate = reader.GetDateTime(reader.GetOrdinal("dueDate")).ToString("yyyy-MM-dd"),
                                    gi_Id = reader.GetString(reader.GetOrdinal("gi_Id")),
                                    customerId = reader.GetString(reader.GetOrdinal("customerId")),
                                    customerName = reader.GetString(reader.GetOrdinal("customerName")),
                                    customerAddress = reader.GetString(reader.GetOrdinal("customerAddress")),
                                    customerNumber = reader.GetString(reader.GetOrdinal("customerNumber")),
                                    total = reader.GetInt32(reader.GetOrdinal("total")),
                                    paid = reader.GetBoolean(reader.GetOrdinal("paid")),
                                    si_Status = reader.GetString(reader.GetOrdinal("si_Status")),
                                    Children = new List<SaleInvoiceItems>()
                                };

                                saleInvoiceDictionary.Add(si_Id, sale);
                            }

                            SaleInvoiceItems saleInvoiceItems = new SaleInvoiceItems
                            {
                                si_ItemId = reader.GetString(reader.GetOrdinal("si_ItemId")),
                                itemId = reader.GetString(reader.GetOrdinal("itemId")),
                                itemName = reader.GetString(reader.GetOrdinal("itemName")),
                                itemQuantity = reader.GetInt32(reader.GetOrdinal("itemQuantity")),
                                uom = reader.GetString(reader.GetOrdinal("uom")),
                                rate = reader.GetInt32(reader.GetOrdinal("rate")),
                                amount = reader.GetInt32(reader.GetOrdinal("amount")),
                                salesInvoice_Id = reader.GetString(reader.GetOrdinal("salesInvoice_Id"))
                            };

                            saleInvoiceDictionary[si_Id].Children.Add(saleInvoiceItems);
                        }

                        resultList.AddRange(saleInvoiceDictionary.Values);
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
