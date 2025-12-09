namespace ClassHub.Models
{
    public class ChatRoomUser
    {
        public int ChatRoomId { get; set; }
        public int UserId { get; set; }

        public ChatRoom? ChatRoom { get; set; }
        public User? User { get; set; }
    }
}
