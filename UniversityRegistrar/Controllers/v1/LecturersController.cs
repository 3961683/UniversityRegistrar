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
    public class LecturersController : ControllerBase
    {
        private readonly UniversityRegistrarContext _context;

        public LecturersController(UniversityRegistrarContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Lecturers.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Lecturers.FindAsync(id));
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] Lecturer model)
        {
            if (model == null)
                return BadRequest(new { errorText = "Invalid input data!" });

            await _context.Lecturers.AddAsync(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Put([FromBody] Lecturer model)
        {
            if (model == null)
                return BadRequest(new { errorText = "Invalid input data!" });

            _context.Update(model);

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id}")]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            Lecturer lecturer = await _context.Lecturers.FindAsync(id);

            if (lecturer != null)
            {
                _context.Lecturers.Remove(lecturer);
                return Ok(await _context.SaveChangesAsync());
            }
            return NotFound(new { error = $"No lecturer with id: {id}" });
        }
    }
}