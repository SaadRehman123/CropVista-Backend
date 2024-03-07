namespace CropVista_Backend.Models
{
    public class Auth
    {
        public int userId { get; set; }
        public string email { get; set; }
        private string tokken { get; set; } 
        public string Tokken
        {
            get { return tokken; }
            set { tokken = value; }
        }
        public string password { get; set; }
        public bool isAuthorized { get; set; }
    }
}
