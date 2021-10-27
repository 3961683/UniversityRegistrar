using UniversityRegistrar.Models;
using UniversityRegistrar.Models.Authentication;
using UniversityRegistrar.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace UniversityRegistrar.Controllers.v1
{
    [Authorize(Roles = UserRoles.Admin)]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UniversityRegistrarContext _context;

        public UsersController(UniversityRegistrarContext context)
        {
            _context = context;
        }

        [EnableCors()]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await Task.Run(() => _context.Users));
        }

        [EnableCors()]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(string id)
        {
            return Ok(await Task.Run(() => _context.Users.FirstOrDefault(u => u.Id == id)));
        }

        [EnableCors()]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] User model)
        {
            if (model == null)
                return BadRequest(new { error = "Invalid input data" });

            if (_context.Users.Select(u => u.Email).ToList().Contains(model.Email))
                return BadRequest(new { error = "This user already exists" });

            if (model.PasswordHash != null)
                model.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash);

            _context.Users.Add(model);
            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [EnableCors()]
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] User model)
        {
            if (model == null)
                return BadRequest(new { error = "Invalid input data" });

            var oldPassword = _context.Users.Where(x => x.Id == model.Id).FirstOrDefault().PasswordHash;

            if (string.IsNullOrWhiteSpace(model.PasswordHash))
                model.PasswordHash = oldPassword;
            else
                model.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.PasswordHash);

            _context.Users.Update(model);

            await _context.SaveChangesAsync();

            return Ok(model);
        }

        [EnableCors()]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            User user = await _context.Users.FindAsync(id);

            if (user != null)
            {
                _context.Users.Remove(user);
                return Ok(await _context.SaveChangesAsync());
            }
            return NotFound(new { error = $"No object with id: {id}" });
        }
    }
}
