using UniversityRegistrar.Models;
using UniversityRegistrar.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using System;

namespace UniversityRegistrar.Controllers.v1
{

    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {

        private UniversityRegistrarContext _context;

        public NewsController(UniversityRegistrarContext context)
        {
            this._context = context;
        }

        [EnableCors()]
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            return Ok(await Task.Run(() => _context.News));
        }

        [HttpPut]
        [EnableCors()]
        public async Task<ActionResult> Put(int id, [FromBody] News news)
        {
            var model = _context.News.FirstOrDefault(e => e.Id == id);

            if (model == null) { return Ok("Wrong input!"); }

            model.Name = news.Name;
            model.Description = news.Description;

            await _context.SaveChangesAsync();

            return Ok(news);
        }


        [EnableCors()]
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] News news)
        {

            _context.News.Add(news);
            await _context.SaveChangesAsync();

            return Ok(news);
        }

        [EnableCors()]
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            News news = await _context.News.FindAsync(id);

            if (news != null)
            {
                _context.News.Remove(news);
                return Ok(await _context.SaveChangesAsync());
            }
            return NotFound(new { error = $"No news with id: {id}" });
        }

    }
}
