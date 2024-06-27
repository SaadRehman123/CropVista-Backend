namespace CropVista_Backend.Models
{
    public class PurchaseRequestItems
    {
        public string PR_itemId { get; set; }
        public string itemId { get; set; }
        public string itemName { get; set; }
        public int itemQuantity { get; set; }
        public string uom { get; set; }
        public string PR_Id { get; set; }
    }
}