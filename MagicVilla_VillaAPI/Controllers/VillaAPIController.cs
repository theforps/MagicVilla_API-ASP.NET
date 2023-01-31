using Microsoft.AspNetCore.Mvc;

[Route("api/VillaApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        return Ok(VillaStore.villaList);
    }

    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}", Name = "GetVilla")]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = VillaStore.villaList.FirstOrDefault(x => x.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        return Ok(villa);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status505HttpVersionNotsupported)]
    public ActionResult<VillaDTO> CreateVilla ([FromBody] VillaDTO villaDTO)
    {
        if (villaDTO == null)
        {
            return BadRequest(villaDTO);
        }
        if (villaDTO.Id > 0) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
        villaDTO.Id = VillaStore.villaList.OrderByDescending(x => x.Id).FirstOrDefault().Id + 1;

        return CreatedAtRoute("GetVilla", new {id = villaDTO.Id}, villaDTO);
    }
}