namespace CropVista_Backend.Models
{
    public class SaleInvoice
    {
        public string salesInvoice_Id { get; set; }
        public string creationDate { get; set; }
        public string dueDate { get; set; }
        public string gi_Id { get; set; }
        public string customerId { get; set; }
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string customerNumber { get; set; }
        public int total { get; set; }
        public bool paid { get; set; }
        public string si_Status { get; set; }

        public List<SaleInvoiceItems> Children { get; set; }

        public SaleInvoice()
        {
            Children = new List<SaleInvoiceItems>();
        }
    }
}
