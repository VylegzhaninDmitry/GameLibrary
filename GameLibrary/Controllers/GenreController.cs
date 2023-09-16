using Microsoft.AspNetCore.Mvc;

namespace GameLibrary.Controllers;

[Route("api/genre")]
[ApiController]
public class GenreController : ControllerBase
{
    private readonly IGenreService _genreService;

    public GenreController(IGenreService genreService)
    {
        _genreService = genreService;
    }

    /// <summary>
    /// Получение всех жанров
    /// </summary>
    /// <returns>Возвращащет список всех жанров</returns>
    [HttpGet("get-genres")]
    public async Task<IActionResult> GetGamesAsync() =>
        Ok(await _genreService.Genres());
}