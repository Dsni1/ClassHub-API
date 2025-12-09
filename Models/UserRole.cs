namespace ClassHub.Models
{
    public class UserRole
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
        public int OrganisationId { get; set; }

        public User? User { get; set; }
        public Role? Role { get; set; }
        public Organisation? Organisation { get; set; }
    }
}
