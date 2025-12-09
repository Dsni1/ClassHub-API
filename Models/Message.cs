namespace ClassHub.Models
{
    public class Message
    {
        public int Id { get; set; }
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }
        public string? Text { get; set; }
        public DateTime Created_At { get; set; }

        public ChatRoom? ChatRoom { get; set; }
        public User? User { get; set; }
    }
}
