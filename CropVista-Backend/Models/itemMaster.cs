namespace CropVista_Backend.Models
{
    public class itemMaster
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemType { get; set; }
        public double SellingRate { get; set; }
        public double ValuationRate { get; set; }
        public bool Disable { get; set; }
        public string UOM { get; set; }
    }
}
