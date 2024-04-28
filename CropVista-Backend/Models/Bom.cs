using System.Security.AccessControl;
using System.Security.Cryptography;

namespace CropVista_Backend.Models
{
    public class Bom
    {
        public string BID { get; set; }
        public string productId { get; set; }
        public string productDescription { get; set; }
        public double productionStdCost { get; set; }
        public int quantity { get; set; }
        public string wrId { get; set; }
        public string priceList { get; set; }
        public double total { get; set; }
        public double productPrice { get; set; }
        public string creationDate { get; set; }

        public List<itemResource> Children { get; set; } 

        public Bom()
        {
            Children = new List<itemResource>();
        }
    }
}
