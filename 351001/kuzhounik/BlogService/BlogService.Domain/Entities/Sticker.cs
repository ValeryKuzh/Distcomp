using Shared.Domain.Interfaces;

namespace BlogService.Domain.Entities;

public class Sticker<Id> : IEntity<Id>
{
    public Id ID { get; set; }
    public string Text { get; set; }
}