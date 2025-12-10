using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ClassHub.Models;

namespace ClassHub.Models;

[Table("UserRoles")]
public class UserRole
{
    [Column("role_id")]
    public int RoleId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }

    [Column("organisation_id")]
    public int OrganisationId { get; set; }

    [ForeignKey(nameof(RoleId))]
    public Role Role { get; set; } = null!;

    [ForeignKey(nameof(UserId))]
    public User User { get; set; } = null!;

    [ForeignKey(nameof(OrganisationId))]
    public Organisation Organisation { get; set; } = null!;
}

