namespace ClientsWebApp.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }

        public User(Guid id, string email, Role role)
        {
            Id = id;
            Email = email;
            Role = role;
        }
    }
}
