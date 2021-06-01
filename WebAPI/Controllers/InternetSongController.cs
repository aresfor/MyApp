using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MyApp.Shared.Models;
using MyApp.WebAPI.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InternetSongController : ControllerBase
    {
        static string imageURL = "https://vignette4.wikia.nocookie.net/vocalopedia/images/b/b5/Original.jpg";
        public readonly MyDbContext context;
        public InternetSongController(MyDbContext myContext)
        {
            context = myContext;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongAsync()
        {
            var res = from s in context.Songs.AsNoTracking()
                      select s;
            if (res == null)
            {
                return BadRequest();
            }
            return await res.ToListAsync();
            //songs.Add(new Song("All Along with you", "EGOIST", "3:44", imageURL));

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Song>> GetSongById(int id)
        {
            var query = from s in context.Songs.AsNoTracking()
                        where s.Id == id
                        select s
                      ;
            var res = await query.FirstOrDefaultAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }
        [HttpPost]
        public async Task<CreatedAtActionResult> AddSong([FromBody] Song value)
        {
            var res = context.Add(value);
            await context.SaveChangesAsync();
            return CreatedAtAction("AddSong", "api/InternetSong", value);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateSong([FromBody]Song value)
        {
            var res = context.Update(value);

            if(res.State == EntityState.Modified)
                await context.SaveChangesAsync();
            else if (res.State == EntityState.Unchanged)
                return BadRequest();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSong(int id)
        {
            var res = context.Remove(id);
            if (res.State == EntityState.Deleted)
                await context.SaveChangesAsync();
            else if (res.State == EntityState.Detached)
                return BadRequest();
            return NotFound();
        }
             
    }
}