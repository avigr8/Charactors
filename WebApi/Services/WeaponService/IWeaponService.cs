using WebApi.DTOs.Character;
using WebApi.DTOs.Weapon;
using WebApi.Migrations;

namespace WebApi.Services.WeaponService;

public interface IWeaponService
{
    Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);
}
