using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs.Fight;

namespace WebApi.Services.FightService;

public class FightService : IFightService
{
    private readonly DataContext _context;

    public FightService(DataContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
    {
        var response = new ServiceResponse<AttackResultDto>();

        try
        {
            var attacker = await _context.Characters
                .Include(w => w.Skills)
                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

            var opponent = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

            var skill = attacker.Skills.FirstOrDefault(s=>s.Id==request.SkillId);

            if (skill == null)
            { 
                response.Success = false;
                response.Message = $"{attacker.Name} doesn't know that skill";
                return response;
            }

            int damage = skill.Damage + (new Random().Next(attacker.Intelligence));
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
                opponent.HitPoints -= damage;

            if (opponent.HitPoints <= 0) response.Message = $"{opponent.Name} has been defeated!";

            await _context.SaveChangesAsync();

            response.Data = new AttackResultDto
            {
                Attacker = attacker.Name,
                Opponent = opponent.Name,
                AttackerHP = attacker.HitPoints,
                OpponentHP = opponent.HitPoints,
                Damage = damage
            };
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
    {
        var response = new ServiceResponse<AttackResultDto>();

        try
        {
            var attacker = await _context.Characters
                .Include(w => w.Weapon)
                .FirstOrDefaultAsync(c => c.Id == request.AttackerId);

            var opponent = await _context.Characters
                .FirstOrDefaultAsync(c => c.Id == request.OpponentId);

            int damage = attacker.Weapon.Damage + (new Random().Next(attacker.Strength));
            damage -= new Random().Next(opponent.Defense);

            if (damage > 0)
                opponent.HitPoints -= damage;

            if (opponent.HitPoints <= 0) response.Message = $"{opponent.Name} has been defeated!";

            await _context.SaveChangesAsync();

            response.Data = new AttackResultDto
            {
                Attacker = attacker.Name,
                Opponent = opponent.Name,
                AttackerHP = attacker.HitPoints,
                OpponentHP = opponent.HitPoints,
                Damage = damage
            };
        }
        catch (Exception ex)
        {
            response.Success = false;
            response.Message = ex.Message;
        }

        return response;
    }
}
