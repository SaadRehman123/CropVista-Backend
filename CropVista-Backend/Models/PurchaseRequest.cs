namespace CropVista_Backend.Models
{
    public class PurchaseRequest
    {
        public string purchaseRequestId { get; set; }
        public string PR_CreationDate { get; set; }
        public string PR_RequiredBy { get; set; }
        public string PR_Status { get; set; }

        public List<PurchaseRequestItems> Children { get; set; }

        public PurchaseRequest()
        {
            Children = new List<PurchaseRequestItems>();
        }
    }
}