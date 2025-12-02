using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;
using WebRestAPI.Interfaces.Area.Common;


namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class AspNetRoleClaimsController : ControllerBase, iCRUD<AspNetRoleClaims>
{
    private WebRestOracleContext _context;
    // Create a field to store the mapper object
    private readonly IMapper _mapper;

    public AspNetRoleClaimsController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.AspNetRoleClaims.ToListAsync();
        if (lst.Count == 0)
        {
            return StatusCode(StatusCodes.Status204NoContent, "No AspNetRoleClaims records to fetch. ");
        } 
        return Ok(lst);
    }


    [HttpGet]
    [Route("Get/{ID}")]
    public async Task<IActionResult> Get(string ID)
    {
        var itm = await _context.AspNetRoleClaims.Where(x => x.AspNetRoleClaimsGuid == ID).FirstOrDefaultAsync();
        if (itm == null)
        {
            return StatusCode(StatusCodes.Status204NoContent, "No AspNetRoleClaims records to fetch. ");
        }        
        return Ok(itm);
    }


    [HttpDelete]
    [Route("Delete/{ID}")]
    public async Task<IActionResult> Delete(string ID)
    {
        var itm = await _context.AspNetRoleClaims.Where(x => x.AspNetRoleClaimsGuid == ID).FirstOrDefaultAsync();
        if (itm == null)
        {
            return StatusCode(StatusCodes.Status204NoContent, "Record not deleted- record does not exist ");
        }
        try
        {
            _context.AspNetRoleClaims.Remove(itm);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Record not deleted. " + ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] AspNetRoleClaims _Item)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.AspNetRoleClaims.AsNoTracking()
            .Where(x => x.AspNetRoleClaimsGuid == _Item.AspNetRoleClaimsGuid)
            .FirstOrDefaultAsync();
            if (itm != null)
            {
                itm = _mapper.Map<AspNetRoleClaims>(_Item);
                _context.AspNetRoleClaims.Update(itm);
                await _context.SaveChangesAsync();
                trans.Commit();

            }
        }
        catch (Exception ex)
        {
            trans.Rollback();
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] AspNetRoleClaims _Item)
    {
        var trans = _context.Database.BeginTransaction();
        try
        {
            _Item.AspNetRoleClaimsGuid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.AspNetRoleClaims.Add(_Item);
            await _context.SaveChangesAsync();
            trans.Commit();
        }
        catch (Exception ex)
        {
            trans.Rollback();
            return StatusCode(StatusCodes.Status400BadRequest, ex.Message);
        }
        return Ok(_Item);
    }
}
