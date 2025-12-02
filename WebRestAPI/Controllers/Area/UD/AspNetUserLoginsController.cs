using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebRest.EF.Data;
using WebRest.EF.Models;
using WebRestAPI.Interfaces.Area.Common;


namespace WebRestAPI.Controllers.UD;

[ApiController]
[Route("api/[controller]")]
public class AspNetUserLoginsController : ControllerBase, iCRUD<AspNetUserLogins>
{
    private WebRestOracleContext _context;
    // Create a field to store the mapper object
    private readonly IMapper _mapper;

    public AspNetUserLoginsController(WebRestOracleContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("Get")]
    public async Task<IActionResult> Get()
    {
        var lst = await _context.AspNetUserLogins.ToListAsync();
        if (lst.Count == 0)
        {
            return StatusCode(StatusCodes.Status204NoContent, "No AspNetUserLogins records to fetch. ");
        } 
        return Ok(lst);
    }


    [HttpGet]
    [Route("Get/{ID}")]
    public async Task<IActionResult> Get(string ID)
    {
        var itm = await _context.AspNetUserLogins.Where(x => x.AspNetUserLoginsGuid == ID).FirstOrDefaultAsync();
        if (itm == null)
        {
            return StatusCode(StatusCodes.Status204NoContent, "No AspNetUserLogins records to fetch. ");
        }        
        return Ok(itm);
    }


    [HttpDelete]
    [Route("Delete/{ID}")]
    public async Task<IActionResult> Delete(string ID)
    {
        var itm = await _context.AspNetUserLogins.Where(x => x.AspNetUserLoginsGuid == ID).FirstOrDefaultAsync();
        if (itm == null)
        {
            return StatusCode(StatusCodes.Status204NoContent, "Record not deleted- record does not exist ");
        }
        try
        {
            _context.AspNetUserLogins.Remove(itm);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, "Record not deleted. " + ex.Message);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Put([FromBody] AspNetUserLogins _Item)
    {
        var trans = _context.Database.BeginTransaction();

        try
        {
            var itm = await _context.AspNetUserLogins.AsNoTracking()
            .Where(x => x.AspNetUserLoginsGuid == _Item.AspNetUserLoginsGuid)
            .FirstOrDefaultAsync();
            if (itm != null)
            {
                itm = _mapper.Map<AspNetUserLogins>(_Item);
                _context.AspNetUserLogins.Update(itm);
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
    public async Task<IActionResult> Post([FromBody] AspNetUserLogins _Item)
    {
        var trans = _context.Database.BeginTransaction();
        try
        {
            _Item.AspNetUserLoginsGuid = Guid.NewGuid().ToString().ToUpper().Replace("-", "");
            _context.AspNetUserLogins.Add(_Item);
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
