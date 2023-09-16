namespace GameLibrary;

public interface IGenreService
{
    Task<IEnumerable<GenreDto>> Genres();
}