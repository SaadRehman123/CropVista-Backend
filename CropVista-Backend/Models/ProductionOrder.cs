namespace CropVista_Backend.Models
{
    public class ProductionOrder
    {
        public string productionOrderId { get; set; }
        public string productionNo { get; set; }
        public string productDescription { get; set; }
        public double productionStdCost { get; set; }
        public int quantity { get; set; }
        public string status { get; set; }
        public string currentDate { get; set; }
        public string startDate { get; set; }
        public string endDate { get; set; }
        public string warehouse { get; set; }

        public List<PO_RouteStages> Children { get; set; }

        public ProductionOrder()
        {
            Children = new List<PO_RouteStages>();
        }
    }
}
