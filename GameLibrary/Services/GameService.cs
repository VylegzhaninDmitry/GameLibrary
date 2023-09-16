using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace GameLibrary;

public class GameService : IGameService
{
    private readonly IEntityFrameworkRepository<Game> _gameRepository;
    private readonly IEntityFrameworkRepository<Genre> _genreRepository;
    private readonly IMapper _mapper;

    public GameService(IEntityFrameworkRepository<Game> gameRepository, IMapper mapper, IEntityFrameworkRepository<Genre> genreRepository)
    {
        _gameRepository = gameRepository;
        _mapper = mapper;
        _genreRepository = genreRepository;
    }

    public async Task<IEnumerable<ReadGameDto>?> GetFilteredGames(FilterParameter filters)
    {
        var query = _gameRepository.GetQueryable().Include(s => s.Genres).AsQueryable();

        if (!string.IsNullOrWhiteSpace(filters.DeveloperStudio))
            query = query.Where(s => s.DeveloperStudio == filters.DeveloperStudio);

        if (filters.Genres.Any())
            query = query.Where(s => s.Genres!.Any(i => filters.Genres.Contains(i.Id)));

        var result = _mapper.Map<List<ReadGameDto>>(await query.ToListAsync());
        
        return result;
    }

    public async Task<ReadGameDto?> GetGameByIdAsync(int id)
    {
        var game = await _gameRepository.GetQueryable().Include(s => s.Genres).FirstOrDefaultAsync(s => s.Id == id);
        if (game is null)
            throw new ValidationException("Игра не найдена");
        
        return _mapper.Map<ReadGameDto>(game);
    }


    public async Task<ReadGameDto?> CreateGameAsync(GameDto dto)
    {
        var game = _mapper.Map<Game>(dto);
        var genres = await _genreRepository.GetQueryable().Where(i => dto.Genres!.Contains(i.Id)).ToListAsync();
        game.Genres = genres;
        await _gameRepository.SaveAsync(game);
        await _gameRepository.SaveChangesAsync();

        return _mapper.Map<ReadGameDto>(game);
    }

    public async Task<bool> DeleteGameAsync(int gameId)
    {
        var game = await _gameRepository.GetByIdAsync(gameId);

        if (game is null)
            throw new ValidationException("Игра не найдена");
        
        _gameRepository.Delete(game);
        await _gameRepository.SaveChangesAsync();

        return true;
    }

    public async Task<IEnumerable<ReadGameDto>> Games() =>
        _mapper.Map<IEnumerable<ReadGameDto>>(await _gameRepository.GetQueryable().Include(s => s.Genres).ToListAsync());

    public async Task<ReadGameDto?> UpdateGameAsync(GameDto dto)
    {
        var game = _mapper.Map<Game>(dto);
        var oldGame = await _gameRepository.GetQueryable()
            .Include(s => s.Genres)
            .Where(s => s.Id == dto.Id)
            .FirstAsync();

        if (oldGame is null)
            throw new ValidationException("Игра не найдена");
        
        oldGame.Genres!.Clear();
        oldGame.DeveloperStudio = game.DeveloperStudio;
        oldGame.Name = game.Name;

        var genres = await _genreRepository.GetQueryable()
            .Where(s => dto.Genres!.Contains(s.Id))
            .ToListAsync();
        oldGame.Genres = new List<Genre>(genres);
        _gameRepository.Update(oldGame);
        await _gameRepository.SaveChangesAsync();
        return _mapper.Map<ReadGameDto>(oldGame);
    }
}