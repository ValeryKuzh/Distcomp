using Shared.Domain.Interfaces;

namespace BlogService.Domain.Entities;

public class Comment<Id> : IEntity<Id>
{
    public Id ID { get; set; }
    
    public Id StoryID { get; set; }
    
    public string Content { get; set; }
}