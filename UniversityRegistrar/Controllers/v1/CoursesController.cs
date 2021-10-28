using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UniversityRegistrar.Models;
using UniversityRegistrar.Models.Entities;

namespace UniversityRegistrar.Controllers.v1
{
[Route("api/[controller]")]
[Authorize]
[EnableCors]
public class CoursesController : ControllerBase
{
    private readonly UniversityRegistrarContext _context;

    public CoursesController(UniversityRegistrarContext context)
    {
        this._context = context;
    }

    [HttpGet]
    public async Task<ActionResult> Get()
    {
/*        var to_delete = _context.Courses.Where(x => x.EndDate < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second));
       
        _context.Courses.Remove(_context.Courses.Find(to_delete.Select(x => x.Id)));

        await _context.SaveChangesAsync();*/

        return Ok(await _context.Courses.ToListAsync());
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _context.Courses.FindAsync(id));
    }

    [HttpPost]
    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> Post([FromBody] Course model)
    {
        if (model == null)
            return BadRequest(new { errorText = "Invalid input data!" });

        if (model.EndDate < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))
            return BadRequest(new { errorText = "End time less than now." });

        await _context.Courses.AddAsync(model);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [HttpPut]
    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> Put([FromBody] Course model)
    {
        if (model == null)
            return BadRequest(new { errorText = "Invalid input data!" });

        if (model.EndDate < new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second))
            return BadRequest(new { errorText = "End time less than now." });

        _context.Update(model);

        await _context.SaveChangesAsync();

        return Ok(model);
    }

    [HttpDelete]
    [Route("{id}")]
    //[Authorize(Roles = "admin")]
    public async Task<IActionResult> Delete(int id)
    {
        _context.Courses.Remove(_context.Courses.Find(id));

        await _context.SaveChangesAsync();

        return NoContent();
    }
    }
}