namespace ClassHub.Models
{
    public class Group
    {
        public int Id { get; set; }
        public int OrganisationId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public Organisation? Organisation { get; set; }
        public ICollection<GroupUser>? GroupUsers { get; set; }
        public ICollection<ChatRoom>? ChatRooms { get; set; }
    }
}
