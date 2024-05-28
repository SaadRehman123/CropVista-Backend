using CropVista_Backend.Models;
using System.Data;
using System.Data.SqlClient;

namespace CropVista_Backend.Services
{
    public class CropsPlanningServices
    {
        public string AddCropsPlan(SqlConnection connection, CropsPlanning cropsPlanning)
        {
            string planId = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateCropPlan", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 1);
                    cmd.Parameters.AddWithValue("@crop", cropsPlanning.crop);
                    cmd.Parameters.AddWithValue("@season", cropsPlanning.season);
                    cmd.Parameters.AddWithValue("@acre", cropsPlanning.acre);
                    cmd.Parameters.AddWithValue("@startdate", DateTime.Parse(cropsPlanning.startdate));
                    cmd.Parameters.AddWithValue("@enddate", DateTime.Parse(cropsPlanning.enddate));
                    cmd.Parameters.AddWithValue("@status", cropsPlanning.status);
                    cmd.Parameters.AddWithValue("@itemId", cropsPlanning.itemId);

                    // Output parameter to capture the generated ID
                    SqlParameter outputParam = new SqlParameter("@id", SqlDbType.NVarChar, 50)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(outputParam);

                    connection.Open();
                    cmd.ExecuteNonQuery();

                    planId = outputParam.Value.ToString();
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

            return planId;
        }
        public CropsPlanning UpdateCropsPlan(SqlConnection connection, CropsPlanning cropsPlanning, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateCropPlan", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 2);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@crop", cropsPlanning.crop);
                    cmd.Parameters.AddWithValue("@season", cropsPlanning.season);
                    cmd.Parameters.AddWithValue("@acre", cropsPlanning.acre);
                    cmd.Parameters.AddWithValue("@startdate", DateTime.Parse(cropsPlanning.startdate));
                    cmd.Parameters.AddWithValue("@enddate", DateTime.Parse(cropsPlanning.enddate));
                    cmd.Parameters.AddWithValue("@status", cropsPlanning.status);
                    cmd.Parameters.AddWithValue("@itemId", cropsPlanning.itemId);

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

            return cropsPlanning;
        }
        public CropsPlanning DeleteCropsPlan(SqlConnection connection, CropsPlanning cropsPlanning, string id)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("CreateCropPlan", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@queryType", 3);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@crop", cropsPlanning.crop);
                    cmd.Parameters.AddWithValue("@season", cropsPlanning.season);
                    cmd.Parameters.AddWithValue("@acre", cropsPlanning.acre);
                    cmd.Parameters.AddWithValue("@startdate", DateTime.Parse(cropsPlanning.startdate));
                    cmd.Parameters.AddWithValue("@enddate", DateTime.Parse(cropsPlanning.enddate));
                    cmd.Parameters.AddWithValue("@status", cropsPlanning.status);
                    cmd.Parameters.AddWithValue("@itemId", cropsPlanning.itemId);

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

            return cropsPlanning;
        }
        public List<CropsPlanning> GetPlannedCrops(SqlConnection connection)
        {
            List<CropsPlanning> plannedCropsList = new List<CropsPlanning>();

            try
            {
                using (SqlCommand command = new SqlCommand("CreateCropPlan", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@queryType", 4);
                    command.Parameters.AddWithValue("@id", "");
                    command.Parameters.AddWithValue("@crop", "");
                    command.Parameters.AddWithValue("@season", "");
                    command.Parameters.AddWithValue("@acre", "");
                    command.Parameters.AddWithValue("@startdate", "");
                    command.Parameters.AddWithValue("@enddate", "");
                    command.Parameters.AddWithValue("@status", "");
                    command.Parameters.AddWithValue("@itemId", "");

                    connection.Open();
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            CropsPlanning cropsPlanning = new CropsPlanning
                            {
                                id = reader.GetString(reader.GetOrdinal("id")),
                                season = reader.GetString(reader.GetOrdinal("season")),
                                crop = reader.GetString(reader.GetOrdinal("crop")),
                                acre = reader.GetInt32(reader.GetOrdinal("acre")),
                                startdate = reader.GetDateTime(reader.GetOrdinal("startdate")).ToString("yyyy-MM-dd"),
                                enddate = reader.GetDateTime(reader.GetOrdinal("enddate")).ToString("yyyy-MM-dd"),
                                status = reader.GetString(reader.GetOrdinal("status")),
                                itemId = reader.GetString(reader.GetOrdinal("itemId"))
                            };

                            plannedCropsList.Add(cropsPlanning);
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

            return plannedCropsList;
        }
    }
}
