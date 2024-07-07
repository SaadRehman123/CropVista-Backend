namespace CropVista_Backend.Models
{
    public class SaleOrderItems
    {
        public string so_ItemId { get; set; }
        public string itemId { get; set; }
        public string itemName { get; set; }
        public int itemQuantity { get; set; }
        public string uom { get; set; }
        public int rate { get; set; }
        public int amount { get; set; }
        public string saleOrder_Id { get; set; }
    }
}
