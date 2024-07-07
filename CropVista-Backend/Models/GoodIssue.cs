namespace CropVista_Backend.Models
{
    public class GoodIssue
    {
        public string gi_Id { get; set; }
        public string saleOrder_Id { get; set; }
        public string creationDate { get; set; }
        public string customerId { get; set; }
        public string customerName { get; set; }
        public string customerAddress { get; set; }
        public string customerNumber { get; set; }
        public int total { get; set; }
        public string gi_Status { get; set; }

        public List<GoodIssueItems> Children { get; set; }

        public GoodIssue()
        {
            Children = new List<GoodIssueItems>();
        }
    }
}
