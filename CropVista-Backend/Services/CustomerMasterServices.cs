using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class CustomerMasterServices
    {
        public string AddCustomer(SqlConnection connection, CustomerMaster customer)
        {
            string customerMasterId = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateCustomerMaster", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 1);
                    cmd.Parameters.AddWithValue("@customerName", customer.customerName);
                    cmd.Parameters.AddWithValue("@customerAddress", customer.customerAddress);
                    cmd.Parameters.AddWithValue("@customerNumber", customer.customerNumber);
                    cmd.Parameters.AddWithValue("@customerEmail", customer.customerEmail);
                    cmd.Parameters.AddWithValue("@disable", customer.disable);

                    // Output parameter to capture the generated ID
                    SqlParameter outputParam = new SqlParameter("@customerId", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    customerMasterId = outputParam.Value.ToString();
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

            return customerMasterId;
        }
        public CustomerMaster UpdateCustomer(SqlConnection connection, CustomerMaster customer, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateCustomerMaster", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 2);
                    cmd.Parameters.AddWithValue("@customerId", id);
                    cmd.Parameters.AddWithValue("@customerName", customer.customerName);
                    cmd.Parameters.AddWithValue("@customerAddress", customer.customerAddress);
                    cmd.Parameters.AddWithValue("@customerNumber", customer.customerNumber);
                    cmd.Parameters.AddWithValue("@customerEmail", customer.customerEmail);
                    cmd.Parameters.AddWithValue("@disable", customer.disable);

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

            return customer;
        }
        public CustomerMaster DeleteCustomer(SqlConnection connection, CustomerMaster customer, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateCustomerMaster", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 3);
                    cmd.Parameters.AddWithValue("@customerId", id);
                    cmd.Parameters.AddWithValue("@customerName", customer.customerName);
                    cmd.Parameters.AddWithValue("@customerAddress", customer.customerAddress);
                    cmd.Parameters.AddWithValue("@customerNumber", customer.customerNumber);
                    cmd.Parameters.AddWithValue("@customerEmail", customer.customerEmail);
                    cmd.Parameters.AddWithValue("@disable", customer.disable);

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

            return customer;
        }
        public List<CustomerMaster> GetCustomerMaster(SqlConnection connection)
        {
            List<CustomerMaster> items = new List<CustomerMaster>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateCustomerMaster", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@customerId", "");
                    command.Parameters.AddWithValue("@customerName", "");
                    command.Parameters.AddWithValue("@customerAddress", "");
                    command.Parameters.AddWithValue("@customerNumber", "");
                    command.Parameters.AddWithValue("@customerEmail", "");
                    command.Parameters.AddWithValue("@disable", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CustomerMaster item = new CustomerMaster
                            {
                                customerId = reader.GetString(reader.GetOrdinal("customerId")),
                                customerName = reader.GetString(reader.GetOrdinal("customerName")),
                                customerAddress = reader.GetString(reader.GetOrdinal("customerAddress")),
                                customerNumber = reader.GetString(reader.GetOrdinal("customerNumber")),
                                customerEmail = reader.GetString(reader.GetOrdinal("customerEmail")),
                                disable  = reader.GetBoolean(reader.GetOrdinal("disable")),
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
