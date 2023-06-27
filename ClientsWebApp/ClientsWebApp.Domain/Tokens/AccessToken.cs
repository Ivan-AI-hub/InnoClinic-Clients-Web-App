namespace ClientsWebApp.Domain.Tokens
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
