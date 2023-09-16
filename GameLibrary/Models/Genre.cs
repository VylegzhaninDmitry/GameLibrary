namespace GameLibrary;

public class Genre : BaseEntity
{
    public string Name { get; set; } = null!;
    
    public List<Game>? Games { get; set; } 
}