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
    public class CoursesController : ControllerBase
    {
        private readonly UniversityRegistrarContext _context;

        public CoursesController(UniversityRegistrarContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult> Get()
        {
            return Ok(await _context.Courses.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult> GetAvilableCourses()
        {
            var courses = await _context.Courses.ToListAsync();

            return Ok(courses.Where(x => x.EndDate > new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _context.Courses.FindAsync(id));
        }

        [HttpPost]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Post([FromBody] Course model)
        {
            if (model == null)
                return BadRequest(new { errorText = "Invalid input data!" });

            if (model.EndDate < model.StartDate)
                return BadRequest(new { errorText = "End time less than now." });

            if (model.StartDate < DateTime.Now)
                return BadRequest(new { errorText = "Start time less than now." });

            await _context.Courses.AddAsync(model);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut]
       // [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Put(int id,[FromBody] Course model)
        {
            Course obj = _context.Courses.Find(id);

            if(obj == null)
            {
                return BadRequest(new { errorText = "Course not found" });
            }

            if (model == null)
                return BadRequest(new { errorText = "Invalid input data!" });

            if (model.StartDate < DateTime.Now)
                return BadRequest(new { errorText = "End time less than now." });

            if (model.EndDate < model.StartDate)
                return BadRequest(new { errorText = "End time less than now." });

            _context.Update(model);

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [HttpDelete]
        [Route("{id}")]
        //[Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> Delete(int id)
        {
            _context.Courses.Remove(_context.Courses.Find(id));

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}