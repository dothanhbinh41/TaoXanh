namespace TaoxanhProxy.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public string AccessToken { get; set; }
        public long ExpiresIn { get; set; }
    }
}
