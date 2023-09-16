namespace GameLibrary;

public class Game : BaseEntity
{
    public string Name { get; set; } = null!;

    public string DeveloperStudio { get; set; } = null!;

    public List<Genre>? Genres { get; set; }

}