using AutoMapper;
using WebApi.DTOs.Character;
using WebApi.DTOs.Skill;
using WebApi.DTOs.Weapon;

namespace WebApi.Profiles;

public class AutoMapperProfile : Profile
{
	public AutoMapperProfile()
	{
		CreateMap<Character, GetCharacterDto>().ReverseMap();
        CreateMap<Character, AddCharacterDto>().ReverseMap();
        CreateMap<Weapon, GetWeaponDto>().ReverseMap();
        CreateMap<Skill, GetSkillDto>().ReverseMap();
    }
}
