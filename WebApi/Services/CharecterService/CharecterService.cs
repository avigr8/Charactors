using AutoMapper;
using WebApi.DTOs.Character;
using WebApi.Models;

namespace WebApi.Services.CharecterService
{
    public class CharecterService : ICharecterService
    {
        private static List<Character> characters = new List<Character> {
            new Character(),
            new Character{Id=1, Name="Kumar"}
        };
        
        private readonly IMapper _mapper;

        public CharecterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddCharacter(AddCharacterDto newCharecter)
        {
            var serviceResponse = new ServiceResponse<List<GetCharacterDto>>();

            Character character = _mapper.Map<Character>(newCharecter);
            character.Id = characters.Max(o => o.Id) + 1;
            characters.Add(character);
            serviceResponse.Data= characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();

            return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> response = new ServiceResponse<List<GetCharacterDto>>();

            try
            {
                Character character = characters.First(o => o.Id == id);
                characters.Remove(character);
                response.Data = characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList();
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
            return new ServiceResponse<List<GetCharacterDto>> { Data= characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToList()};
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetById(int id)
        {
            var serviceResponse = new ServiceResponse<GetCharacterDto>();
            var character = characters.FirstOrDefault(c => c.Id == id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(character);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updateCharacterDto)
        {
            ServiceResponse<GetCharacterDto> response=new ServiceResponse<GetCharacterDto>();

            try
            {
                Character character = characters.FirstOrDefault(o => o.Id == updateCharacterDto.Id);

                //character.Name = updateCharacterDto.Name;
                //character.HitPoints = updateCharacterDto.HitPoints;
                //character.Strength = updateCharacterDto.Strength;
                //character.Defense = updateCharacterDto.Defense;
                //character.Intelligence = updateCharacterDto.Intelligence;
                //character.Class = updateCharacterDto.Class;

                _mapper.Map(updateCharacterDto,character);

                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch(Exception ex)
            {
                response.Success = false;
                response.Message= ex.Message;
            }

            return response;
        }
    }
}
