using System.ComponentModel.DataAnnotations.Schema;

namespace ClassHub.Models;


[Table("GroupUsers")]
public class GroupUser
{
    [Column("group_id")]
    public int GroupId { get; set; }

    [Column("user_id")]
    public int UserId { get; set; }
}
