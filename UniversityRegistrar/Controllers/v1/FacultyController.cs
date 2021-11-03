using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using UniversityRegistrar.Models;
using UniversityRegistrar.Models.Authentication;
using UniversityRegistrar.Models.Entities;

namespace UniversityRegistrar.Controllers.v1
{
    [Route("api/[controller]")]
    // [Authorize]
    [EnableCors]
    public class FacultyController : ControllerBase
    {
        private readonly UniversityRegistrarContext _context;

        public FacultyController(UniversityRegistrarContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Faculties.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Faculties.FindAsync(id));
        }

        [HttpPost]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Post([FromBody] Faculty model)
        {
            if (model == null)
                return BadRequest(new { errorText = "Invalid input data!" });

            await _context.Faculties.AddAsync(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        //[Authorize(Roles = UserRoles.Admin + "," + UserRoles.Lecturer)]
        public async Task<IActionResult> Put([FromBody] Faculty model)
        {
            if (model == null)
                return BadRequest(new { errorText = "Invalid input data!" });

            _context.Update(model);

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id}")]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            Faculty faculty = await _context.Faculties.FindAsync(id);

            if (faculty != null)
            {
                _context.Faculties.Remove(faculty);
                return Ok(await _context.SaveChangesAsync());
            }
            return NotFound(new { error = $"No Faculty with id: {id}" });
        }
    }
}