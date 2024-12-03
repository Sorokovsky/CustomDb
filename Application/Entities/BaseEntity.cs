using Database.Attributes;

namespace Application.Entities;

public class BaseEntity
{
    [Key] public int Id { get; set; }
}