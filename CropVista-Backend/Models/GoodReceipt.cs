namespace CropVista_Backend.Models
{
    public class GoodReceipt
    {
        public string gr_Id { get; set; }
        public string pro_Id { get; set; }
        public string vendorId { get; set; }
        public string vendorName { get; set; }
        public string vendorAddress { get; set; }
        public string vendorNumber { get; set; }
        public string creationDate { get; set; }
        public int total { get; set; }
        public string gr_Status { get; set; }

        public List<GoodReceiptItems> Children { get; set; }

        public GoodReceipt()
        {
            Children = new List<GoodReceiptItems>();
        }
    }
}
