using WebApi.DTOs.Character;
using WebApi.Services.CharecterService;

namespace WebApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]

public class CharecterController : ControllerBase
{
    private readonly ICharecterService _charecterService;

    public CharecterController(ICharecterService charecterService)
    {
        _charecterService = charecterService;
    }

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
    {
        return Ok(await _charecterService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Get(int id)
    {

        return Ok(await _charecterService.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
    {
        return Ok(await _charecterService.AddCharacter(newCharacter));
    }
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updateCharacter)
    {
        var response = await _charecterService.UpdateCharacter(updateCharacter);

        if (response.Data == null)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
    {
        var response = await _charecterService.DeleteCharacter(id);

        if (response.Data == null)
            return NotFound(response);

        return Ok(response);
    }

    [HttpPost("Skill")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> AddCharacterSkill(AddCharacterSkillDto newCharacterSkill)
    {
        var response=await _charecterService.AddCharacterSkill(newCharacterSkill);

        if(response.Data== null) 
            return NotFound(response);

        return Ok(response);
    }
}
