namespace GameLibrary;

public class GameDto
{
    public int? Id { get; set; }
    
    public string? Name { get; set; }

    public string? DeveloperStudio { get; set; }

    public List<int>? Genres { get; set; }
}