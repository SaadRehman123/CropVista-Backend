namespace CropVista_Backend.Models
{
    public class SaleOrder
    {
        public string saleOrder_Id { get; set; }
        public string creationDate { get; set; }
        public string deliveryDate { get; set; }
        public string customerId { get; set; }
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string customerNumber { get; set; }
        public int total { get; set; }
        public string so_Status { get; set; }

        public List<SaleOrderItems> Children { get; set; }

        public SaleOrder()
        {
            Children = new List<SaleOrderItems>();
        }
    }
}
