namespace CropVista_Backend.Models
{
    public class VendorQuotationItems
    {
        public string vq_ItemId { get; set; }
        public string itemId { get; set; }
        public string itemName { get; set; }
        public int itemQuantity { get; set; }
        public string uom { get; set; }
        public int rate { get; set; }
        public int amount { get; set; }
        public string vq_Id { get; set; }
    }
}
