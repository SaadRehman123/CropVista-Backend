namespace CropVista_Backend.Models
{
    public class RequestForQuotationItem
    {
        public string rfq_ItemId { get; set; }
        public string itemId { get; set; }
        public string itemName { get; set; }
        public int itemQuantity { get; set; }
        public string uom { get; set; }
        public string rfq_Id { get; set; }
    }
}
