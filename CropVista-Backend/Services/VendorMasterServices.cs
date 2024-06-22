using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;
using System.Numerics;

namespace CropVista_Backend.Services
{
    public class VendorMasterServices
    {
        public string AddVendor(SqlConnection connection, VendorMaster vendor)
        {
            string vendorMasterId = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateVendorMaster", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 1);
                    cmd.Parameters.AddWithValue("@vendorName", vendor.vendorName);
                    cmd.Parameters.AddWithValue("@vendorGroup", vendor.vendorGroup);
                    cmd.Parameters.AddWithValue("@vendorType", vendor.vendorType);
                    cmd.Parameters.AddWithValue("@isDisabled", vendor.isDisabled);
                    cmd.Parameters.AddWithValue("@vendorAddress", vendor.vendorAddress);
                    cmd.Parameters.AddWithValue("@vendorNumber", vendor.vendorNumber);
                    cmd.Parameters.AddWithValue("@vendorEmail", vendor.vendorEmail);

                    // Output parameter to capture the generated ID
                    SqlParameter outputParam = new SqlParameter("@vendorId", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    vendorMasterId = outputParam.Value.ToString();
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

            return vendorMasterId;
        }
        public VendorMaster UpdateVendor(SqlConnection connection, VendorMaster vendor, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateVendorMaster", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 2);
                    cmd.Parameters.AddWithValue("@vendorId", id);
                    cmd.Parameters.AddWithValue("@vendorName", vendor.vendorName);
                    cmd.Parameters.AddWithValue("@vendorGroup", vendor.vendorGroup);
                    cmd.Parameters.AddWithValue("@vendorType", vendor.vendorType);
                    cmd.Parameters.AddWithValue("@isDisabled", vendor.isDisabled);
                    cmd.Parameters.AddWithValue("@vendorAddress", vendor.vendorAddress);
                    cmd.Parameters.AddWithValue("@vendorNumber", vendor.vendorNumber);
                    cmd.Parameters.AddWithValue("@vendorEmail", vendor.vendorEmail);

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

            return vendor;
        }
        public VendorMaster DeleteVendor(SqlConnection connection, VendorMaster vendor, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateVendorMaster", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 3);
                    cmd.Parameters.AddWithValue("@vendorId", id);
                    cmd.Parameters.AddWithValue("@vendorName", vendor.vendorName);
                    cmd.Parameters.AddWithValue("@vendorGroup", vendor.vendorGroup);
                    cmd.Parameters.AddWithValue("@vendorType", vendor.vendorType);
                    cmd.Parameters.AddWithValue("@isDisabled", vendor.isDisabled);
                    cmd.Parameters.AddWithValue("@vendorAddress", vendor.vendorAddress);
                    cmd.Parameters.AddWithValue("@vendorNumber", vendor.vendorNumber);
                    cmd.Parameters.AddWithValue("@vendorEmail", vendor.vendorEmail);


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

            return vendor;
        }
        public List<VendorMaster> GetVendorMaster(SqlConnection connection)
        {
            List<VendorMaster> items = new List<VendorMaster>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateVendorMaster", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@vendorId", "");
                    command.Parameters.AddWithValue("@vendorName", "");
                    command.Parameters.AddWithValue("@vendorGroup", "");
                    command.Parameters.AddWithValue("@vendorType", "");
                    command.Parameters.AddWithValue("@isDisabled", "");
                    command.Parameters.AddWithValue("@vendorAddress", "");
                    command.Parameters.AddWithValue("@vendorNumber", "");
                    command.Parameters.AddWithValue("@vendorEmail", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            VendorMaster item = new VendorMaster
                            {
                                vendorId = reader.GetString(reader.GetOrdinal("vendorId")),
                                vendorName = reader.GetString(reader.GetOrdinal("vendorName")),
                                vendorGroup = reader.GetString(reader.GetOrdinal("vendorGroup")),
                                vendorType = reader.GetString(reader.GetOrdinal("vendorType")),
                                isDisabled = reader.GetBoolean(reader.GetOrdinal("isDisabled")),
                                vendorAddress = reader.GetString(reader.GetOrdinal("vendorAddress")),
                                vendorNumber = reader.GetString(reader.GetOrdinal("vendorNumber")),
                                vendorEmail = reader.GetString(reader.GetOrdinal("vendorEmail"))
                            };

                            items.Add(item);
                        }
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

            return items;
        }
    }
}
