namespace ClientsWebApp.Shared
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Channel { get; set; }
        public Guid CreatorId { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
