using MagicVilla_VillaAPI.Logging;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/VillaApi")]
[ApiController]
public class VillaApiController : ControllerBase
{
    //custom logger
    //private readonly ILogging _logger;
    //public VillaApiController(ILogging logger) 
    //{
    //    _logger = logger;
    //}
    private readonly ApplicationDbContext _applicationDb;
    public VillaApiController(ApplicationDbContext applicationDb)
    {
        _applicationDb = applicationDb;
    }

    #region Get
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<VillaDTO>> GetVillas()
    {
        //_logger.Log("Getting all villas", "");
        return Ok(_applicationDb.Villas.ToList());
    }
    #endregion

    #region Get by id
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{id:int}", Name = "GetVilla")]
    public ActionResult<VillaDTO> GetVilla(int id)
    {
        if (id == 0)
        {
            //_logger.Log("Get Villa Error with Id " + id, "error");
            return BadRequest();
        }
        var villa = _applicationDb.Villas.FirstOrDefault(x => x.Id == id);
        if (villa == null)
        {
            return NotFound();
        }
        return Ok(villa);
    }
    #endregion

    #region Post
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status505HttpVersionNotsupported)]
    public ActionResult<VillaDTO> CreateVilla ([FromBody] VillaDTO villaDTO)
    {
        if (_applicationDb.Villas.FirstOrDefault(x => x.Name.ToLower() == villaDTO.Name.ToLower()) != null)
        {
            ModelState.AddModelError("CustomError", "Villa already Exists!");
            return BadRequest(ModelState);
        }
        if (villaDTO == null)
        {
            return BadRequest(villaDTO);
        }
        if (villaDTO.Id > 0) 
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }

        var villa = new Villa()
        {
            Id = villaDTO.Id,
            Name = villaDTO.Name,
            Details = villaDTO.Details,
            Amenity = villaDTO.Amenity,
            ImageUrl = villaDTO.ImageUrl,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqrt = villaDTO.Sqrt,
            CreatedTime = DateTime.Now,
            UpdatedTime = DateTime.Now
        };

        _applicationDb.Villas.Add(villa);
        _applicationDb.SaveChanges();

        return CreatedAtRoute("GetVilla", new {id = villaDTO.Id}, villaDTO);
    }
    #endregion

    #region Delete by id
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [HttpDelete("{id:int}", Name = "DeleteVilla")]
    public IActionResult DeleteVilla(int id)
    {
        if (id == 0)
        {
            return BadRequest();
        }
        var villa = _applicationDb.Villas.FirstOrDefault(x=> x.Id == id);
        if (villa == null)
        {
            return NotFound();
        }

        _applicationDb.Villas.Remove(villa);
        _applicationDb.SaveChanges();

        return NoContent();
    }
    #endregion

    #region PUT
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPut("{id:int}", Name = "UpdateVilla")]
    public ActionResult UpdateVilla(int id, [FromBody]VillaDTO villaDTO)
    {
        if (id == null || id == 0 || id != villaDTO.Id)
        {
            return BadRequest();
        }

        var villa = new Villa()
        {
            Id = villaDTO.Id,
            Name = villaDTO.Name,
            Details = villaDTO.Details,
            Amenity = villaDTO.Amenity,
            ImageUrl = villaDTO.ImageUrl,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqrt = villaDTO.Sqrt,
            CreatedTime = DateTime.Now,
            UpdatedTime = DateTime.Now
        };

        _applicationDb.Villas.Update(villa);
        _applicationDb.SaveChanges();

        return NoContent();
    }
    #endregion

    #region PATCH
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpPatch("{id:int}", Name = "UpdatePartialVilla")]
    public ActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDTO> patchDTO)
    {
        if (patchDTO == null || id == 0)
        {
            return BadRequest();
        }

        var villa = _applicationDb.Villas.AsNoTracking().FirstOrDefault(x => x.Id == id);

        var villaDTO = new VillaDTO()
        {
            Id = villa.Id,
            Name = villa.Name,
            Details = villa.Details,
            Amenity = villa.Amenity ,
            ImageUrl = villa.ImageUrl,
            Occupancy = villa.Occupancy,
            Rate = villa.Rate,
            Sqrt = villa.Sqrt
        };

        if (villa == null)
        {
            return BadRequest();
        }

        patchDTO.ApplyTo(villaDTO, ModelState);

        var model = new Villa()
        {
            Id = villaDTO.Id,
            Name = villaDTO.Name,
            Details = villaDTO.Details,
            Amenity = villaDTO.Amenity,
            ImageUrl = villaDTO.ImageUrl,
            Occupancy = villaDTO.Occupancy,
            Rate = villaDTO.Rate,
            Sqrt = villaDTO.Sqrt,
        };

        _applicationDb.Villas.Update(model);
        _applicationDb.SaveChanges();

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return NoContent();
    }
    #endregion
}