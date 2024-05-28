using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class CropsServices
    {
        public Crops AddCrop(SqlConnection connection, Crops crops)
        {
            using (SqlCommand cmd = new SqlCommand("INSERT INTO crops (cropId, name, season) VALUES ('" + crops.cropId + "', '" + crops.name + "', '" + crops.season + "')", connection))
            {
                connection.Open();
                int i = cmd.ExecuteNonQuery();
                connection.Close();
            }

            return crops;
        }

        public List<Crops> GetCropsBySeason(SqlConnection connection, string season)
        {
            List<Crops> cropsList = new List<Crops>();

            using (SqlCommand command = new SqlCommand("SELECT * FROM crops WHERE season = @season", connection))
            {
                command.Parameters.AddWithValue("@season", season);

                using (SqlDataAdapter dataAdapter = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    dataAdapter.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            Crops crops = new Crops
                            {
                                cropId = Convert.ToString(dt.Rows[i]["cropId"]),
                                name = Convert.ToString(dt.Rows[i]["name"]),
                                season = Convert.ToString(dt.Rows[i]["season"]),
                            };

                            cropsList.Add(crops);
                        }
                    }
                }
            }

            return cropsList;
        }
    }
}
