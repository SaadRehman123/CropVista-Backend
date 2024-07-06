namespace CropVista_Backend.Models
{
    public class PurchaseInvoice
    {
        public string pi_Id { get; set; }
        public string dueDate { get; set; }
        public string creationDate { get; set; }
        public string gr_Id { get; set; }
        public string vendorId { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorNumber { get; set; }
        public string pi_Status { get; set; }
        public bool paid { get; set; }
        public int total { get; set; }

        public List<PurchaseInvoiceItems> Children { get; set; }

        public PurchaseInvoice()
        {
            Children = new List<PurchaseInvoiceItems>();
        }
    }
}
