namespace CropVista_Backend.Models
{
    public class VendorQuotation
    {
        public string vq_Id { get; set; }
        public string vq_CreationDate { get; set; }
        public string rfq_Id { get; set; }
        public string vendorId { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorNumber { get; set; }
        public string vq_Status { get; set; }
        public int total { get; set; }

        public List<VendorQuotationItems> Children { get; set; }

        public VendorQuotation()
        {
            Children = new List<VendorQuotationItems>();
        }
    }
}
