namespace CropVista_Backend.Models
{
    public class itemResource
    {
        public string BID { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int quantity { get; set; }
        public string UOM { get; set; }
        public string warehouseId { get; set; }
        public double unitPrice { get; set; }
        public double total { get; set; }
        public int routeSequence { get; set; }
        public string priceList { get; set; }
    }
}
