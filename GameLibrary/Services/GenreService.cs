using AutoMapper;

namespace GameLibrary;

public class GenreService : IGenreService
{
    private readonly IEntityFrameworkRepository<Genre> _genreRepository;
    private readonly IMapper _mapper;

    public GenreService(IEntityFrameworkRepository<Genre> genreRepository, IMapper mapper)
    {
        _genreRepository = genreRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<GenreDto>> Genres()
    {
      return  _mapper.Map<IEnumerable<GenreDto>>(await _genreRepository.GetAllAsync());
    }
}