namespace CropVista_Backend.Models
{
    public class CropsPlanning
    {
        public string id { get; set; }
        public string season { get; set; }
        public string crop { get; set; }
        public int acre { get; set; }
        public string startdate { get; set; }
        public string enddate { get; set; }
        public string status { get; set; }
        public string itemId { get; set; }
    }
}
