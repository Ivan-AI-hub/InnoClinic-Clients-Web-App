namespace ClientsWebApp.Domain.Tokens
{
    public class SecurityToken
    {
        public string AccessToken { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public DateTime ExpireAt { get; set; }

        public SecurityToken(string accessToken, string email, Role role, DateTime expireAt)
        {
            AccessToken = accessToken;
            Email = email;
            Role = role;
            ExpireAt = expireAt;
        }
    }
}
