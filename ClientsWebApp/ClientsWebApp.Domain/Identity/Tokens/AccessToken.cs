namespace ClientsWebApp.Domain.Identity.Tokens
{
    public class AccessToken
    {
        public string Token { get; set; }

        public AccessToken(string token)
        {
            Token = token;
        }
    }
}
