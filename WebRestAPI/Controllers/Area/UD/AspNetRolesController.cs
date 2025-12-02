using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;
using WebRestAPI.Interfaces.Area.Common;


namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class AspNetRolesController : ControllerBase, iCRUD<AspNetRoles>
{
    private WebRestOracleContext _context;
    // Create a field to store the mapper object
    private readonly IMapper _mapper;

    public AspNetRolesController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.AspNetRoles.ToListAsync();
        if (lst.Count == 0)
        {
            return StatusCode(StatusCodes.Status204NoContent, "No AspNetRoles records to fetch. ");
        } 
        return Ok(lst);
    }


    [HttpGet]
    [Route("Get/{ID}")]
    public async Task<IActionResult> Get(string ID)
    {
        var itm = await _context.AspNetRoles.Where(x => x.AspNetRolesGuid == ID).FirstOrDefaultAsync();
        if (itm == null)
        {
            return StatusCode(StatusCodes.Status204NoContent, "No AspNetRoles records to fetch. ");
        }        
        return Ok(itm);
    }


    [HttpDelete]
    [Route("Delete/{ID}")]
    public async Task<IActionResult> Delete(string ID)
    {
        var itm = await _context.AspNetRoles.Where(x => x.AspNetRolesGuid == ID).FirstOrDefaultAsync();
        if (itm == null)
        {
            return StatusCode(StatusCodes.Status204NoContent, "Record not deleted- record does not exist ");
        }
        try
        {
            _context.AspNetRoles.Remove(itm);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Record not deleted. " + ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] AspNetRoles _Item)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.AspNetRoles.AsNoTracking()
            .Where(x => x.AspNetRolesGuid == _Item.AspNetRolesGuid)
            .FirstOrDefaultAsync();
            if (itm != null)
            {
                itm = _mapper.Map<AspNetRoles>(_Item);
                _context.AspNetRoles.Update(itm);
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
    public async Task<IActionResult> Post([FromBody] AspNetRoles _Item)
    {
        var trans = _context.Database.BeginTransaction();
        try
        {
            _Item.AspNetRolesGuid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.AspNetRoles.Add(_Item);
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
