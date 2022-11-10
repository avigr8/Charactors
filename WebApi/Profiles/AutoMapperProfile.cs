using AutoMapper;
using WebApi.DTOs.Character;

namespace WebApi.Profiles;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		CreateMap<Character, GetCharacterDto>().ReverseMap();
        CreateMap<Character, AddCharacterDto>().ReverseMap();
        CreateMap<Character, UpdateCharacterDto>().ReverseMap();
    }
}
