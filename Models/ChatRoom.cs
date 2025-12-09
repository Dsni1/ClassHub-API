namespace ClassHub.Models
{
    public class ChatRoom
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Type { get; set; } // 'private' or 'group'
        public DateTime Created_At { get; set; }

        public int? GroupId { get; set; }
        public Group? Group { get; set; }

        public ICollection<ChatRoomUser>? ChatRoomUsers { get; set; }
        public ICollection<Message>? Messages { get; set; }
    }
}
