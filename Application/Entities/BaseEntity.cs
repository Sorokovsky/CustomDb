using Database.Attributes;

namespace Application.Entities;

public class BaseEntity
{
    [PrimaryKey] public int Id { get; set; }
}