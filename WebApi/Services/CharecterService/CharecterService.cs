using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.DTOs.Character;
using WebApi.Models;

namespace WebApi.Services.CharecterService
{
    public class CharecterService : ICharecterService
    {
        //private static List<Character> characters = new List<Character> {
        //    new Character(),
        //    new Character{Id=1, Name="Kumar"}
        //};

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharecterService(IMapper mapper, DataContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharecter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            Character character = _mapper.Map<Character>(newCharecter);
            _context.Characters.Add(character);
            await _context.SaveChangesAsync();

            serviceResponse.Data = await _context.Characters
                .Select(c => _mapper.Map<GetCharacterDto>(c))
                .ToListAsync();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character character = await _context.Characters.FirstAsync(o => o.Id == id);
                
                _context.Characters.Remove(character);
                await _context.SaveChangesAsync();

                response.Data = await _context.Characters
                    .Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAll()
        {
            var response=new ServiceResponse<List<GetCharacterDto>>();
            var dbCharacters = await _context.Characters.ToListAsync();

            response.Data = dbCharacters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
            
            return response;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var dbcharacter =await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(dbcharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();

            try
            {
                var character = await _context.Characters
                    .FirstOrDefaultAsync(o => o.Id == updateCharacterDto.Id);

                character.Name = updateCharacterDto.Name;
                character.HitPoints = updateCharacterDto.HitPoints;
                character.Strength = updateCharacterDto.Strength;
                character.Defense = updateCharacterDto.Defense;
                character.Intelligence = updateCharacterDto.Intelligence;
                character.Class = updateCharacterDto.Class;

                //_mapper.Map(updateCharacterDto, character);

                await _context.SaveChangesAsync();

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }

            return response;
        }
    }
}
