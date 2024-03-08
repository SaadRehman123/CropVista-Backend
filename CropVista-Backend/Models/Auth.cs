namespace CropVista_Backend.Models
{
    public class Auth
    {
        public int userId { get; set; }
        public string email { get; set; }
        private string token { get; set; } 
        public string Token
        {
            get { return token; }
            set { token = value; }
        }
        public string password { get; set; }
        public bool isAuthorized { get; set; }
    }
}
