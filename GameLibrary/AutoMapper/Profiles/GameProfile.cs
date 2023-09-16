using AutoMapper;

namespace GameLibrary.AutoMapper.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameDto>().ForMember(dest => dest.Genres, opt => opt.Ignore());
        CreateMap<GameDto, Game>().ForMember(dest => dest.Genres, opt => opt.Ignore());
        CreateMap<Game, ReadGameDto>()
            .ForMember(dest => dest.Genres,
                opt => opt.MapFrom(src => src.Genres));
    }
}