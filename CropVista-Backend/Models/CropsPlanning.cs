namespace CropVista_Backend.Models
{
    public class CropsPlanning
    {
        public int id { get; set; }
        public string season { get; set; }
        public string crop { get; set; }
        public int acre { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
    }
}
