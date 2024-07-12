using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class VendorQuotationServices
    {
        public string AddVendorQuotation(SqlConnection connection, VendorQuotation vendorQuotation)
        {
            string vq_Id = "";

            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateVendorQuotation", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@vq_CreationDate", vendorQuotation.vq_CreationDate);
                        cmd.Parameters.AddWithValue("@rfq_Id", vendorQuotation.rfq_Id);
                        cmd.Parameters.AddWithValue("@vendorId", vendorQuotation.vendorId);
                        cmd.Parameters.AddWithValue("@vendorName", vendorQuotation.vendorName);
                        cmd.Parameters.AddWithValue("@vendorAddress", vendorQuotation.vendorAddress);
                        cmd.Parameters.AddWithValue("@vendorNumber", vendorQuotation.vendorNumber);
                        cmd.Parameters.AddWithValue("@vq_Status", vendorQuotation.vq_Status);
                        cmd.Parameters.AddWithValue("@total", vendorQuotation.total);

                        // Output parameter to capture the generated ID
                        SqlParameter outputParam = new SqlParameter("@vq_Id", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        vq_Id = outputParam.Value.ToString();
                    }

                    foreach (var item in vendorQuotation.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateVendorQuotationItem", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 1);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@vq_Id", vq_Id);

                            SqlParameter outputParam = new SqlParameter("@vq_ItemId", SqlDbType.NVarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            item.vq_ItemId = outputParam.Value.ToString();
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

            return vq_Id;
        }
        public void UpdateVendorQuotation(SqlConnection connection, VendorQuotation vendorQuotation, string vq_Id)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateVendorQuotation", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 2);
                        cmd.Parameters.AddWithValue("@vq_Id", vq_Id);
                        cmd.Parameters.AddWithValue("@vq_CreationDate", vendorQuotation.vq_CreationDate);
                        cmd.Parameters.AddWithValue("@rfq_Id", vendorQuotation.rfq_Id);
                        cmd.Parameters.AddWithValue("@vendorId", vendorQuotation.vendorId);
                        cmd.Parameters.AddWithValue("@vendorName", vendorQuotation.vendorName);
                        cmd.Parameters.AddWithValue("@vendorAddress", vendorQuotation.vendorAddress);
                        cmd.Parameters.AddWithValue("@vendorNumber", vendorQuotation.vendorNumber);
                        cmd.Parameters.AddWithValue("@vq_Status", vendorQuotation.vq_Status);
                        cmd.Parameters.AddWithValue("@total", vendorQuotation.total);

                        cmd.ExecuteNonQuery();
                    }

                    foreach (var item in vendorQuotation.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateVendorQuotationItem", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 2);
                            cmd.Parameters.AddWithValue("@vq_ItemId", item.vq_ItemId);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@vq_Id", vq_Id);

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
        public void DeleteVendorQuotation(SqlConnection connection, VendorQuotation vendorQuotation, string vq_Id)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    foreach (var item in vendorQuotation.Children)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateVendorQuotationItem", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 3);
                            cmd.Parameters.AddWithValue("@vq_ItemId", item.vq_ItemId);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rate", item.rate);
                            cmd.Parameters.AddWithValue("@amount", item.amount);
                            cmd.Parameters.AddWithValue("@vq_Id", vq_Id);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    using (SqlCommand cmd = new SqlCommand("CreateVendorQuotation", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 3);
                        cmd.Parameters.AddWithValue("@vq_Id", vq_Id);
                        cmd.Parameters.AddWithValue("@vq_CreationDate", vendorQuotation.vq_CreationDate);
                        cmd.Parameters.AddWithValue("@rfq_Id", vendorQuotation.rfq_Id);
                        cmd.Parameters.AddWithValue("@vendorId", vendorQuotation.vendorId);
                        cmd.Parameters.AddWithValue("@vendorName", vendorQuotation.vendorName);
                        cmd.Parameters.AddWithValue("@vendorAddress", vendorQuotation.vendorAddress);
                        cmd.Parameters.AddWithValue("@vendorNumber", vendorQuotation.vendorNumber);
                        cmd.Parameters.AddWithValue("@vq_Status", vendorQuotation.vq_Status);
                        cmd.Parameters.AddWithValue("@total", vendorQuotation.total);

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
        public List<VendorQuotation> GetVendorQuotation(SqlConnection connection, string vq_Id)
        {
            List<VendorQuotation> resultList = new List<VendorQuotation>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateVendorQuotation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@vq_Id", vq_Id);
                    command.Parameters.AddWithValue("@vq_CreationDate", "");
                    command.Parameters.AddWithValue("@rfq_Id", "");
                    command.Parameters.AddWithValue("@vendorId", "");
                    command.Parameters.AddWithValue("@vendorName", "");
                    command.Parameters.AddWithValue("@vendorAddress", "");
                    command.Parameters.AddWithValue("@vendorNumber", "");
                    command.Parameters.AddWithValue("@vq_Status", "");
                    command.Parameters.AddWithValue("@total", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, VendorQuotation> vendorQuotationDictionary = new Dictionary<string, VendorQuotation>();

                        while (reader.Read())
                        {
                            string vendorQuotationId = reader.GetString(reader.GetOrdinal("vq_Id"));

                            if (!vendorQuotationDictionary.ContainsKey(vendorQuotationId))
                            {
                                VendorQuotation vendorQuotation = new VendorQuotation
                                {
                                    vq_Id = vendorQuotationId,
                                    vq_CreationDate = reader.GetDateTime(reader.GetOrdinal("vq_CreationDate")).ToString("yyyy-MM-dd"),
                                    rfq_Id = reader.GetString(reader.GetOrdinal("rfq_Id")),
                                    vendorId = reader.GetString(reader.GetOrdinal("vendorId")),
                                    vendorName = reader.GetString(reader.GetOrdinal("vendorName")),
                                    vendorAddress = reader.GetString(reader.GetOrdinal("vendorAddress")),
                                    vendorNumber = reader.GetString(reader.GetOrdinal("vendorNumber")),
                                    vq_Status = reader.GetString(reader.GetOrdinal("vq_Status")),
                                    total = reader.GetInt32(reader.GetOrdinal("total")),
                                    Children = new List<VendorQuotationItems>()
                                };

                                vendorQuotationDictionary.Add(vendorQuotationId, vendorQuotation);
                            }

                            VendorQuotationItems vendorQuotationItems = new VendorQuotationItems
                            {
                                vq_ItemId = reader.GetString(reader.GetOrdinal("vq_ItemId")),
                                itemId = reader.GetString(reader.GetOrdinal("itemId")),
                                itemName = reader.GetString(reader.GetOrdinal("itemName")),
                                itemQuantity = reader.GetInt32(reader.GetOrdinal("itemQuantity")),
                                uom = reader.GetString(reader.GetOrdinal("uom")),
                                rate = reader.GetInt32(reader.GetOrdinal("rate")),
                                amount = reader.GetInt32(reader.GetOrdinal("amount")),
                                vq_Id = reader.GetString(reader.GetOrdinal("vq_Id"))
                            };

                            vendorQuotationDictionary[vendorQuotationId].Children.Add(vendorQuotationItems);
                        }

                        resultList.AddRange(vendorQuotationDictionary.Values);
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
