namespace GameLibrary;

public interface IGameService
{
    Task<IEnumerable<ReadGameDto>?> GetFilteredGames(FilterParameter filters);

    Task<ReadGameDto?> GetGameByIdAsync(int id);

    Task<ReadGameDto?> CreateGameAsync(GameDto dto);

    Task<bool> DeleteGameAsync(int bookId);

    Task<IEnumerable<ReadGameDto>> Games();
    
    Task<ReadGameDto?> UpdateGameAsync(GameDto dto);
}