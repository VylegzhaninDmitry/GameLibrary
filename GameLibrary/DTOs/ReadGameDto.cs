namespace GameLibrary;

public class ReadGameDto
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? DeveloperStudio { get; set; }

    public List<GenreDto>? Genres { get; set; }
}