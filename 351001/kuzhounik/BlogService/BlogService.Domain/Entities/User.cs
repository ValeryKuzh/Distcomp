using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shared.Domain.Interfaces;

namespace BlogService.Domain.Entities;

[Table("tbl_user")]
public class User<Id> : IEntity<Id>
{
    [Key]
    [Column("id")]
    public Id ID { get; set; }
    [Column("login")]
    public string Login { get; set; }
    [Column("password")]
    public string Password { get; set; }
    [Column("firstname")]
    public string FirstName { get; set; }
    [Column("lastname")]
    public string LastName { get; set; }

    public ICollection<Story<Id>> Stories { get; set; } = new List<Story<Id>>();
}