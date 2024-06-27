using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class RequestForQuotationServices
    {
        public string AddRequestForQuotation(SqlConnection connection, RequestForQuotation requestForQuotation)
        {
            string requestForQuotationId = "";

            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateRequestForQuotation", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@rfq_CreationDate", requestForQuotation.rfq_CreationDate);
                        cmd.Parameters.AddWithValue("@rfq_RequiredBy", requestForQuotation.rfq_RequiredBy);
                        cmd.Parameters.AddWithValue("@rfq_Status", requestForQuotation.rfq_Status);
                        cmd.Parameters.AddWithValue("@pr_Id", requestForQuotation.pr_Id);

                        // Output parameter to capture the generated ID
                        SqlParameter outputParam = new SqlParameter("@rfq_Id", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        requestForQuotationId = outputParam.Value.ToString();
                    }

                    foreach (var item in requestForQuotation.ChildrenItems)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateRequestForQuotationItem", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 1);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rfq_Id", requestForQuotationId);

                            SqlParameter outputParam = new SqlParameter("@rfq_ItemId", SqlDbType.NVarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            item.rfq_ItemId = outputParam.Value.ToString();
                        }
                    }

                    foreach (var vendor in requestForQuotation.ChildrenVendors)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateRequestForQuotationVendor", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 1);
                            cmd.Parameters.AddWithValue("@vendorId", vendor.vendorId);
                            cmd.Parameters.AddWithValue("@vendorName", vendor.vendorName);
                            cmd.Parameters.AddWithValue("@vendorNumber", vendor.vendorNumber);
                            cmd.Parameters.AddWithValue("@rfq_Id", requestForQuotationId);

                            SqlParameter outputParam = new SqlParameter("@rfq_VendorId", SqlDbType.NVarChar, 50)
                            {
                                Direction = ParameterDirection.Output
                            };
                            cmd.Parameters.Add(outputParam);

                            cmd.ExecuteNonQuery();

                            vendor.rfq_VendorId = outputParam.Value.ToString();
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

            return requestForQuotationId;
        }
        public void AddRequestForQuotationItems(SqlConnection connection, List<RequestForQuotationItem> requestForQuotationItems, string rfq_Id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateRequestForQuotationItem", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    foreach (var item in requestForQuotationItems)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@itemId", item.itemId);
                        cmd.Parameters.AddWithValue("@itemName", item.itemName);
                        cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                        cmd.Parameters.AddWithValue("@uom", item.uom);
                        cmd.Parameters.AddWithValue("@rfq_Id", item.rfq_Id);

                        SqlParameter outputParam = new SqlParameter("@rfq_ItemId", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        item.rfq_ItemId = outputParam.Value.ToString();
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
        public void AddRequestForQuotationVendors(SqlConnection connection, List<RequestForQuotationVendor> requestForQuotationVendors, string rfq_Id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateRequestForQuotationVendor", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();

                    foreach (var vendor in requestForQuotationVendors)
                    {
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@queryType", 1);
                        cmd.Parameters.AddWithValue("@vendorId", vendor.vendorId);
                        cmd.Parameters.AddWithValue("@vendorName", vendor.vendorName);
                        cmd.Parameters.AddWithValue("@vendorNumber", vendor.vendorNumber);
                        cmd.Parameters.AddWithValue("@rfq_Id", vendor.rfq_Id);

                        SqlParameter outputParam = new SqlParameter("@rfq_VendorId", SqlDbType.NVarChar, 50)
                        {
                            Direction = ParameterDirection.Output
                        };
                        cmd.Parameters.Add(outputParam);

                        cmd.ExecuteNonQuery();

                        vendor.rfq_VendorId = outputParam.Value.ToString();
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
        public void DeleteRequestForQuotationItems(SqlConnection connection, List<RequestForQuotationItem> requestForQuotationItems)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    foreach (var item in requestForQuotationItems)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateRequestForQuotationItem", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 3);
                            cmd.Parameters.AddWithValue("@rfq_ItemId", item.rfq_ItemId);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rfq_Id", item.rfq_Id);

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
        public void DeleteRequestForQuotationVendors(SqlConnection connection, List<RequestForQuotationVendor> requestForQuotationVendors)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    foreach (var vendor in requestForQuotationVendors)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateRequestForQuotationVendor", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 3);
                            cmd.Parameters.AddWithValue("@rfq_VendorId", vendor.rfq_VendorId);
                            cmd.Parameters.AddWithValue("@vendorId", vendor.vendorId);
                            cmd.Parameters.AddWithValue("@vendorName", vendor.vendorName);
                            cmd.Parameters.AddWithValue("@vendorNumber", vendor.vendorNumber);
                            cmd.Parameters.AddWithValue("@rfq_Id", vendor.rfq_Id);

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
        public void UpdateRequestForQuotation(SqlConnection connection, RequestForQuotation requestForQuotation, string rfq_Id)
        {
            try
            {
                connection.Open();

                using (SqlTransaction transaction = connection.BeginTransaction())
                {
                    using (SqlCommand cmd = new SqlCommand("CreateRequestForQuotation", connection, transaction))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@queryType", 2);
                        cmd.Parameters.AddWithValue("@rfq_Id", rfq_Id);
                        cmd.Parameters.AddWithValue("@rfq_CreationDate", requestForQuotation.rfq_CreationDate);
                        cmd.Parameters.AddWithValue("@rfq_RequiredBy", requestForQuotation.rfq_RequiredBy);
                        cmd.Parameters.AddWithValue("@rfq_Status", requestForQuotation.rfq_Status);
                        cmd.Parameters.AddWithValue("@pr_Id", requestForQuotation.pr_Id);

                        cmd.ExecuteNonQuery();
                    }

                    foreach (var item in requestForQuotation.ChildrenItems)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateRequestForQuotationItem", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 2);
                            cmd.Parameters.AddWithValue("@rfq_ItemId", item.rfq_ItemId);
                            cmd.Parameters.AddWithValue("@itemId", item.itemId);
                            cmd.Parameters.AddWithValue("@itemName", item.itemName);
                            cmd.Parameters.AddWithValue("@itemQuantity", item.itemQuantity);
                            cmd.Parameters.AddWithValue("@uom", item.uom);
                            cmd.Parameters.AddWithValue("@rfq_Id", item.rfq_Id);

                            cmd.ExecuteNonQuery();
                        }
                    }

                    foreach (var vendor in requestForQuotation.ChildrenVendors)
                    {
                        using (SqlCommand cmd = new SqlCommand("CreateRequestForQuotationVendor", connection, transaction))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@queryType", 2);
                            cmd.Parameters.AddWithValue("@rfq_VendorId", vendor.rfq_VendorId);
                            cmd.Parameters.AddWithValue("@vendorId", vendor.vendorId);
                            cmd.Parameters.AddWithValue("@vendorName", vendor.vendorName);
                            cmd.Parameters.AddWithValue("@vendorNumber", vendor.vendorNumber);
                            cmd.Parameters.AddWithValue("@rfq_Id", vendor.rfq_Id);

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
        public List<RequestForQuotation> GetRequestForQuotation(SqlConnection connection, string rfq_Id)
        {
            List<RequestForQuotation> resultList = new List<RequestForQuotation>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateRequestForQuotation", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@rfq_Id", rfq_Id);
                    command.Parameters.AddWithValue("@rfq_CreationDate", "");
                    command.Parameters.AddWithValue("@rfq_RequiredBy", "");
                    command.Parameters.AddWithValue("@rfq_Status", "");
                    command.Parameters.AddWithValue("@pr_Id", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, RequestForQuotation> requestForQuotationDictionary = new Dictionary<string, RequestForQuotation>();

                        while (reader.Read())
                        {
                            string requestForQuotationId = reader.GetString(reader.GetOrdinal("rfq_Id"));

                            if (!requestForQuotationDictionary.ContainsKey(requestForQuotationId))
                            {
                                RequestForQuotation requestForQuotation = new RequestForQuotation
                                {
                                    rfq_Id = requestForQuotationId,
                                    rfq_CreationDate = reader.GetDateTime(reader.GetOrdinal("rfq_CreationDate")).ToString("yyyy-MM-dd"),
                                    rfq_RequiredBy = reader.GetDateTime(reader.GetOrdinal("rfq_RequiredBy")).ToString("yyyy-MM-dd"),
                                    rfq_Status = reader.GetString(reader.GetOrdinal("rfq_Status")),
                                    pr_Id = reader.GetString(reader.GetOrdinal("pr_Id")),
                                    ChildrenItems = new List<RequestForQuotationItem>(),
                                    ChildrenVendors = new List<RequestForQuotationVendor>()
                                };

                                requestForQuotationDictionary.Add(requestForQuotationId, requestForQuotation);
                            }

                            RequestForQuotationItem requestForQuotationItem = new RequestForQuotationItem
                            {
                                rfq_ItemId = reader.GetString(reader.GetOrdinal("rfq_ItemId")),
                                itemId = reader.GetString(reader.GetOrdinal("itemId")),
                                itemName = reader.GetString(reader.GetOrdinal("itemName")),
                                itemQuantity = reader.GetInt32(reader.GetOrdinal("itemQuantity")),
                                uom = reader.GetString(reader.GetOrdinal("uom")),
                                rfq_Id = reader.GetString(reader.GetOrdinal("rfq_Id"))
                            };

                            RequestForQuotationVendor requestForQuotationVendor = new RequestForQuotationVendor
                            {
                                rfq_VendorId = reader.GetString(reader.GetOrdinal("rfq_VendorId")),
                                vendorId = reader.GetString(reader.GetOrdinal("vendorId")),
                                vendorName = reader.GetString(reader.GetOrdinal("vendorName")),
                                vendorNumber = reader.GetString(reader.GetOrdinal("vendorNumber")),
                                rfq_Id = reader.GetString(reader.GetOrdinal("rfq_Id"))
                            };

                            requestForQuotationDictionary[requestForQuotationId].ChildrenItems.Add(requestForQuotationItem);
                            requestForQuotationDictionary[requestForQuotationId].ChildrenVendors.Add(requestForQuotationVendor);
                        }

                        resultList.AddRange(requestForQuotationDictionary.Values);
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
