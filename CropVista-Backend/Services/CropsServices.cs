using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class CropsServices
    {
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
