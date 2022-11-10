using WebApi.DTOs.Character;
using WebApi.Services.CharecterService;

namespace WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class CharecterController : ControllerBase
{
    private readonly ICharecterService charecterService;

    public CharecterController(ICharecterService _charecterService)
    {
        charecterService = _charecterService;
    }


    [HttpGet]
    [Route("GetAll")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Get()
    {
        return Ok(await charecterService.GetAll());
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> Get(int id)
    {
        
        return Ok(await charecterService.GetById(id));
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> AddCharacter(AddCharacterDto newCharacter)
    {
        return Ok(await charecterService.AddCharacter(newCharacter));
    }
    [HttpPut]
    public async Task<ActionResult<ServiceResponse<GetCharacterDto>>> UpdateCharacter(UpdateCharacterDto updateCharacter)
    {
        var response = await charecterService.UpdateCharacter(updateCharacter);

        if (response.Data == null)
            return NotFound(response);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<ServiceResponse<List<GetCharacterDto>>>> Delete(int id)
    {
        var response = await charecterService.DeleteCharacter(id);

        if (response.Data == null)
            return NotFound(response);

        return Ok(response);
    }
}
