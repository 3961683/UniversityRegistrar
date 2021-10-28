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
    public class StudentsController : ControllerBase
    {
        private readonly UniversityRegistrarContext _context;

        public StudentsController(UniversityRegistrarContext context)
        {
            this._context = context;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Students.ToListAsync());
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Students.FindAsync(id));
        }

        [HttpPost]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Post([FromBody] Student model)
        {
            if (model == null)
                return BadRequest(new { errorText = "Invalid input data!" });

            await _context.Students.AddAsync(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
        //[Authorize(Roles = "admin")]
        public async Task<IActionResult> Put([FromBody] Student model)
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
            Student student = await _context.Students.FindAsync(id);

            if (student != null)
            {
                _context.Students.Remove(student);
                return Ok(await _context.SaveChangesAsync());
            }
            return NotFound(new { error = $"No student with id: {id}" });
        }
    }
}