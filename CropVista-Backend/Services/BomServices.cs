using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;
using System.Security.AccessControl;
using System.Collections.Generic;
using System.Dynamic;

namespace CropVista_Backend.Services
{
    public class BomServices
    {
        public string AddBom(SqlConnection connection, Bom bom)
        {
            string BID = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateBom", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 1);
                    cmd.Parameters.AddWithValue("@productId", bom.productId);
                    cmd.Parameters.AddWithValue("@productDescription", bom.productDescription);
                    cmd.Parameters.AddWithValue("@productionStdCost", bom.productionStdCost);
                    cmd.Parameters.AddWithValue("@quantity", bom.quantity);
                    cmd.Parameters.AddWithValue("@wrId", bom.wrId);
                    cmd.Parameters.AddWithValue("@total", bom.total);
                    cmd.Parameters.AddWithValue("@productPrice", bom.productPrice);
                    cmd.Parameters.AddWithValue("@creationDate", bom.creationDate);
                    cmd.Parameters.AddWithValue("@itemBID", "");
                    
                    // Output parameter to capture the generated ID
                    SqlParameter outputParam = new SqlParameter("@BID", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    BID = outputParam.Value.ToString();
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

            return BID;
        }
        public Bom UpdateBom(SqlConnection connection, Bom bom, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateBom", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 2);
                    cmd.Parameters.AddWithValue("@BID", id);
                    cmd.Parameters.AddWithValue("@productId", bom.productId);
                    cmd.Parameters.AddWithValue("@productDescription", bom.productDescription);
                    cmd.Parameters.AddWithValue("@productionStdCost", bom.productionStdCost);
                    cmd.Parameters.AddWithValue("@quantity", bom.quantity);
                    cmd.Parameters.AddWithValue("@wrId", bom.wrId);
                    cmd.Parameters.AddWithValue("@total", bom.total);
                    cmd.Parameters.AddWithValue("@productPrice", bom.productPrice);
                    cmd.Parameters.AddWithValue("@creationDate", bom.creationDate);
                    cmd.Parameters.AddWithValue("@itemBID", "");

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

            return bom;
        }
        public Bom DeleteBom(SqlConnection connection, Bom bom, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateBom", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 3);
                    cmd.Parameters.AddWithValue("@BID", id);
                    cmd.Parameters.AddWithValue("@productId", bom.productId);
                    cmd.Parameters.AddWithValue("@productDescription", bom.productDescription);
                    cmd.Parameters.AddWithValue("@productionStdCost", bom.productionStdCost);
                    cmd.Parameters.AddWithValue("@quantity", bom.quantity);
                    cmd.Parameters.AddWithValue("@wrId", bom.wrId);
                    cmd.Parameters.AddWithValue("@total", bom.total);
                    cmd.Parameters.AddWithValue("@productPrice", bom.productPrice);
                    cmd.Parameters.AddWithValue("@creationDate", bom.creationDate);
                    cmd.Parameters.AddWithValue("@itemBID", "");

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

            return bom;
        }
        public List<Bom> GetBom(SqlConnection connection, string itemBID)
        {
            List<Bom> resultList = new List<Bom>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateBom", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@BID", "");
                    command.Parameters.AddWithValue("@productId", "");
                    command.Parameters.AddWithValue("@productDescription", "");
                    command.Parameters.AddWithValue("@productionStdCost", "");
                    command.Parameters.AddWithValue("@quantity", "");
                    command.Parameters.AddWithValue("@wrId", "");
                    command.Parameters.AddWithValue("@total", "");
                    command.Parameters.AddWithValue("@productPrice", "");
                    command.Parameters.AddWithValue("@creationDate", "");
                    command.Parameters.AddWithValue("@itemBID", itemBID);

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        Dictionary<string, Bom> bomDictionary = new Dictionary<string, Bom>();

                        while (reader.Read())
                        {
                            string bomId = reader.GetString(reader.GetOrdinal("bid"));

                            if (!bomDictionary.ContainsKey(bomId))
                            {
                                Bom bom = new Bom
                                {
                                    BID = bomId,
                                    productId = reader.GetString(reader.GetOrdinal("productId")),
                                    productDescription = reader.GetString(reader.GetOrdinal("productDescription")),
                                    productionStdCost = (float)reader.GetDouble(reader.GetOrdinal("productionStdCost")),
                                    quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                    wrId = reader.GetString(reader.GetOrdinal("wrId")),
                                    total = (float)reader.GetDouble(reader.GetOrdinal("total")),
                                    productPrice = (float)reader.GetDouble(reader.GetOrdinal("productPrice")),
                                    creationDate = reader.GetDateTime(reader.GetOrdinal("creationDate")).ToString("yyyy-MM-dd"),
                                    Children = new List<itemResource>() 
                                };

                                bomDictionary.Add(bomId, bom);
                            }

                            itemResource resource = new itemResource
                            {
                                BID = reader.GetString(reader.GetOrdinal("BID")),
                                id = reader.GetString(reader.GetOrdinal("id")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                type = reader.GetString(reader.GetOrdinal("type")),
                                quantity = reader.GetInt32(reader.GetOrdinal("quantity")),
                                UOM = reader.GetString(reader.GetOrdinal("UOM")),
                                warehouseId = reader.GetString(reader.GetOrdinal("warehouseId")),
                                unitPrice = (float)reader.GetDouble(reader.GetOrdinal("unitPrice")),
                                total = (float)reader.GetDouble(reader.GetOrdinal("total")),
                                routeSequence = reader.GetInt32(reader.GetOrdinal("routeSequence")),
                                itemResourceId = reader.GetString(reader.GetOrdinal("itemResourceId"))
                            };

                            bomDictionary[bomId].Children.Add(resource);
                        }

                        resultList.AddRange(bomDictionary.Values);
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
