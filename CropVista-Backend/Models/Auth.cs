namespace CropVista_Backend.Models
{
    public class Auth
    {
        public int id { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public bool isAuthorized { get; set; }
    }
}
