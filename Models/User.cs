namespace ClassHub.Models
{
    public class User
    {
        public int Id { get; set; }
        public string? User_Name { get; set; }
        public string? Password { get; set; }

        // Many-to-many with Roles, Organizations
        public ICollection<UserRole>? UserRoles { get; set; }

        // Group memberships
        public ICollection<GroupUser>? GroupUsers { get; set; }

        // Chatroom memberships
        public ICollection<ChatRoomUser>? ChatRoomUsers { get; set; }

        // Messages sent by user
        public ICollection<Message>? Messages { get; set; }
    }
}
