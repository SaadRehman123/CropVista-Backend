namespace CropVista_Backend.Models
{
    public class PurchaseOrder
    {
        public string pro_Id { get; set; }
        public string creationDate { get; set; }
        public string requiredBy { get; set; }
        public string pr_Id { get; set; }
        public string vendorId { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorNumber { get; set; }
        public int total { get; set; }
        public string purchaseOrderStatus { get; set; }

        public List<PurchaseOrderItems> Children { get; set; }

        public PurchaseOrder()
        {
            Children = new List<PurchaseOrderItems>();
        }
    }
}