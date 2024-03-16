using CropVista_Backend.Models;
using System.Data;
using System.Data.SqlClient;

namespace CropVista_Backend.Services
{
    public class CropsPlanningServices
    {
        public List<CropsPlanning> GetPlannedCrops(SqlConnection connection)
        {
            List<CropsPlanning> plannedCropsList = new List<CropsPlanning>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM plannedcrops", connection))
            {
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        CropsPlanning cropsPlanning = new CropsPlanning
                        {
                            id = Convert.ToInt32(dt.Rows[i]["id"]),
                            season = Convert.ToString(dt.Rows[i]["season"]),
                            crop = Convert.ToString(dt.Rows[i]["crop"]),
                            acre = Convert.ToInt32(dt.Rows[i]["acre"]),
                            startdate = Convert.ToString(dt.Rows[i]["startdate"]),
                            enddate = Convert.ToString(dt.Rows[i]["enddate"])
                        };

                        plannedCropsList.Add(cropsPlanning);
                    }
                }
            }

            return plannedCropsList;
        }

        public CropsPlanning AddCropsPlan(SqlConnection connection, CropsPlanning cropsPlanning)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO plannedcrops (crop, season, acre, startdate, enddate) VALUES ('" + cropsPlanning.crop + "', '" + cropsPlanning.season + "', '" + cropsPlanning.acre + "', '" + cropsPlanning.startdate + "', '" + cropsPlanning.enddate + "')", connection))
            {
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
            }

            return cropsPlanning;
        }

        public CropsPlanning UpdateCropsPlan(SqlConnection connection, CropsPlanning cropsPlanning, int id)
        {
            using (SqlCommand cmd = new SqlCommand("UPDATE plannedcrops SET crop = @crop, season = @season, acre = @acre, startdate = @startdate, enddate = @enddate WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@crop", cropsPlanning.crop);
                cmd.Parameters.AddWithValue("@season", cropsPlanning.season);
                cmd.Parameters.AddWithValue("@acre", cropsPlanning.acre);
                cmd.Parameters.AddWithValue("@startdate", cropsPlanning.startdate);
                cmd.Parameters.AddWithValue("@enddate", cropsPlanning.enddate);
                cmd.Parameters.AddWithValue("@id", id);

                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
            }

            return cropsPlanning;
        }

        public CropsPlanning DeleteCropsPlan(SqlConnection connection, CropsPlanning cropsPlanning, int id)
        {
            using (SqlCommand cmd = new SqlCommand("DELETE FROM plannedcrops WHERE id = @id", connection))
            {
                cmd.Parameters.AddWithValue("@crop", cropsPlanning.crop);
                cmd.Parameters.AddWithValue("@season", cropsPlanning.season);
                cmd.Parameters.AddWithValue("@acre", cropsPlanning.acre);
                cmd.Parameters.AddWithValue("@startdate", cropsPlanning.startdate);
                cmd.Parameters.AddWithValue("@enddate", cropsPlanning.enddate);
                cmd.Parameters.AddWithValue("@id", id);

                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
            }

            return cropsPlanning;
        }
    }
}
