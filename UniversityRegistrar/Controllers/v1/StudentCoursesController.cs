using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UniversityRegistrar.Models;
using UniversityRegistrar.Models.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UniversityRegistrar.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentCoursesController : ControllerBase
    {
        private readonly UniversityRegistrarContext _context;

        public StudentCoursesController(UniversityRegistrarContext context)
        {
            _context = context;
        }

        [EnableCors()]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await Task.Run(() => _context.StudentCourses));
        }

        [EnableCors()]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetByStudentId(int id)
        {
            return Ok(await Task.Run(() => _context.StudentCourses.Where(sc => sc.StudentId == id)));
        }

        [EnableCors()]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] StudentCourse model)
        {
            if (model == null)
                return BadRequest(new { error = "Invalid input data" });

            if (!_context.Students.Select(s => s.Id).ToList().Contains(model.StudentId))
                return BadRequest(new { error = "This student does not exist" });

            if (!_context.Courses.Select(c => c.Id).ToList().Contains(model.CourseId))
                return BadRequest(new { error = "This course does not exist" });

            _context.StudentCourses.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [EnableCors()]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] StudentCourse model)
        {
            if (model == null)
                return BadRequest(new { error = "Invalid input data" });

            _context.StudentCourses.Update(model);

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [EnableCors()]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            StudentCourse studentCourse = await _context.StudentCourses.FindAsync(id);

            if (studentCourse != null)
            {
                _context.StudentCourses.Remove(studentCourse);
                return Ok(await _context.SaveChangesAsync());
            }
            return NotFound(new { error = $"No object with id: {id}" });
        }
    }
}
