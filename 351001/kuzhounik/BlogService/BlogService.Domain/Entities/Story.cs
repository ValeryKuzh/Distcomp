using Shared.Domain.Interfaces;

namespace BlogService.Domain.Entities;

public class Story<Id> : IEntity<Id>
{
    public Id ID { get; set; }
    public Id UserID { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
}