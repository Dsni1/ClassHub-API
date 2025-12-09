namespace ClassHub.Models
{
    public class Organisation
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public ICollection<UserRole>? UserRoles { get; set; }
        public ICollection<Group>? Groups { get; set; }
    }
}
