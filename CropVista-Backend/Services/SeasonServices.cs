using CropVista_Backend.Models;
using System.Data.SqlClient;
using System.Data;

namespace CropVista_Backend.Services
{
    public class SeasonServices
    {
        public List<Seasons> GetSeasons(SqlConnection connection)
        {
            List<Seasons> seasonsList = new List<Seasons>();

            using (SqlDataAdapter dataAdapter = new SqlDataAdapter("SELECT * FROM seasons", connection))
            {
                DataTable dt = new DataTable();
                dataAdapter.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        Seasons seasons = new Seasons
                        {
                            seasons = Convert.ToString(dt.Rows[i]["seasons"])
                        };

                        seasonsList.Add(seasons);
                    }
                }
            }

            return seasonsList;
        }
    }
}
