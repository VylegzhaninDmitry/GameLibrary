using AutoMapper;

namespace GameLibrary.AutoMapper.Profiles;

public class GenreProfile : Profile
{
    public GenreProfile()
    {
        CreateMap<GenreDto, Genre>().ForMember(dest => dest.Games, opt => opt.Ignore());
        CreateMap<Genre, GenreDto>();
    }
}