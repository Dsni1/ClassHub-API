using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClassHub.Models;

namespace ClassHub.Models;

[Table("Organisations")]
public class Organisation
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name")]
    [MaxLength(255)]
    public string Name { get; set; } = null!;

    public ICollection<Group> Groups { get; set; } = new List<Group>();
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}

