using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class ResourceServices
    {
        public string AddResource(SqlConnection connection, Resource resource)
        {
            string resourceId = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateResource", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 1);
                    cmd.Parameters.AddWithValue("@name", resource.name);
                    cmd.Parameters.AddWithValue("@rType", resource.rType);

                    // Output parameter to capture the generated ID
                    SqlParameter outputParam = new SqlParameter("@rId", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    resourceId = outputParam.Value.ToString();
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

            return resourceId;
        }
        public Resource UpdateResource(SqlConnection connection, Resource resource, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateResource", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 2);
                    cmd.Parameters.AddWithValue("@rId", id);
                    cmd.Parameters.AddWithValue("@name", resource.name);
                    cmd.Parameters.AddWithValue("@rType", resource.rType);

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

            return resource;
        }
        public Resource DeleteResource(SqlConnection connection, Resource resource, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateResource", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 3);
                    cmd.Parameters.AddWithValue("@rId", id);
                    cmd.Parameters.AddWithValue("@name", resource.name);
                    cmd.Parameters.AddWithValue("@rType", resource.rType);

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

            return resource;
        }
        public List<Resource> GetResources(SqlConnection connection)
        {
            List<Resource> resources = new List<Resource>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateResource", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@rId", "");
                    command.Parameters.AddWithValue("@name", "");
                    command.Parameters.AddWithValue("@rType", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            Resource resource = new Resource
                            {
                                rId = reader.GetString(reader.GetOrdinal("rId")),
                                name = reader.GetString(reader.GetOrdinal("name")),
                                rType = reader.GetString(reader.GetOrdinal("rType"))
                            };

                            resources.Add(resource);
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

            return resources;
        }
    }
}
