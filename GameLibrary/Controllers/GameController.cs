using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers;

[Route("api/game")]
[ApiController]
public class GameController : ControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    /// <summary>
    /// Получение данных о игре по id
    /// </summary>
    /// <param name="id">id игры</param>
    /// <returns>Возвращает игру по id, если null то выводит сообщение</returns>
    [HttpGet("get/{id:int}")]
    public async Task<IActionResult> GetGameByIdAsync(int id) =>
        Ok(await _gameService.GetGameByIdAsync(id));

    /// <summary>
    /// Получение отфилтрованного списка игр
    /// </summary>
    /// <param name="filters">Параметры для фильтра</param>
    /// <returns>Возвращает отфилтрованный список игр</returns>
    [HttpPost("filter")]
    public async Task<IActionResult> FilterAsync([FromBody] FilterParameter filters) =>
        Ok(await _gameService.GetFilteredGames(filters));
    
    /// <summary>
    /// Сохранение игры
    /// </summary>
    /// <param name="dto">Дто игры</param>
    /// <returns>Возвращащет созданную игру</returns>
    [HttpPost("create-game")]
    public async Task<IActionResult> CreateGameAsync([FromBody] GameDto dto) =>
        Ok(await _gameService.CreateGameAsync(dto));
    
    /// <summary>
    /// Удаление игры
    /// </summary>
    /// <param name="id">Идентификатор игры</param>
    /// <returns>Возвращает true, если игра была удалена</returns>
    [HttpDelete("delete/{id:int}")]
    public async Task<IActionResult> DeleteGameAsync(int id) => 
        Ok(await _gameService.DeleteGameAsync(id));

    
    
    /// <summary>
    /// Получение всех игр
    /// </summary>
    /// <returns>Возвращащет список всех игр</returns>
    [HttpGet("get-games")]
    public async Task<IActionResult> GetGamesAsync() =>
        Ok(await _gameService.Games());
    
    /// <summary>
    /// Обновление игры
    /// </summary>
    /// <param name="dto">Дто игры</param>
    /// <returns>Возвращащет обновленную игру</returns>
    [HttpPut("update-game")]
    public async Task<IActionResult> UpdateGameAsync([FromBody] GameDto dto) =>
        Ok(await _gameService.UpdateGameAsync(dto));
}