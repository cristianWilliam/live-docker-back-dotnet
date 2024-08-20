#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace back_dotnet.Db.Models;

internal sealed class Todo
{
    public Guid Id { get; private init; }
    public string Name { get; private init; }
    public DateTime AddedDate { get; private init; }
    
    public Todo(string name, Guid id)
    {
        Id = id;
        Name = name;
        AddedDate = DateTime.UtcNow;
    }
    
    // EF Constructor
    private Todo(){} 
}