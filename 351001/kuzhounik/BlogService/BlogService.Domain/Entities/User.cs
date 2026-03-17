using Shared.Domain.Interfaces;

namespace BlogService.Domain.Entities;

public class User<Id> : IEntity<Id>
{
    public Id ID { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
}