namespace CropVista_Backend.Models
{
    public class RequestForQuotation
    {
        public string rfq_Id { get; set; }
        public string rfq_CreationDate { get; set; }
        public string pr_Id { get; set; }
        public string rfq_RequiredBy { get; set; }
        public string rfq_Status { get; set; }

        public List<RequestForQuotationItem> ChildrenItems { get; set; }
        public List<RequestForQuotationVendor> ChildrenVendors { get; set; }

        public RequestForQuotation()
        {
            ChildrenItems = new List<RequestForQuotationItem>();
            ChildrenVendors = new List<RequestForQuotationVendor>();
        }
    }
}
