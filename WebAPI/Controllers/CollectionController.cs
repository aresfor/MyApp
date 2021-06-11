using Microsoft.AspNetCore.Mvc;
using MyApp.Shared.Models;
using MyApp.WebAPI.Context;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MyApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CollectionController : ControllerBase
    {
        MyDbContext context;
        public CollectionController(MyDbContext _context)
        {
            context = _context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Collection>>> GetCollection()
        {
            var res = await context.Collections.ToListAsync();
            return res;
        }
        [HttpGet("{dif}/{CollectionId}")]
        public async Task<ActionResult<IEnumerable<Song>>> GetSongFromColletions([FromRoute] int dif,[FromRoute] int CollectionId)
        {
            var query = from a in context.Collections
                       where a.CollectionId == CollectionId
                       select (
                           from c in a.songs
                           select c
                       );


            var res = await query.FirstOrDefaultAsync();
            return res.ToList();
        }
        [HttpGet("{AccountId}")]
        public async Task<ActionResult<IEnumerable<Collection>>> GetAccountCollections([FromRoute] int AccountId)
        {
            var query = from a in context.Accounts
                        where a.AccountId == AccountId
                        select (
                            from c in a.Collecitons
                            select c
                        );


            var res = await query.FirstOrDefaultAsync();
            return res.ToList();
        }
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Collection>> GetCollectionById([FromRoute] int id)
        //{
        //    var query = from c in context.Collections
        //                where c.CollectionId == id
        //                select c
        //              ;
        //    var res = await query.FirstOrDefaultAsync();
        //    if (res == null)
        //    {
        //        return NotFound();
        //    }
        //    return res;
        //}
        [HttpPost("{AccountName}/{CollectionName}")]
        public async Task<IActionResult> AddCollection([FromRoute] string AccountName, [FromRoute] string CollectionName)
        {
            var account = await context.Accounts.FindAsync(AccountName);
            var collection = new Collection { Name = CollectionName };
            context.Collections.Add(collection);
            //有必要？
            var c = await context.Collections.FindAsync(CollectionName);
            account.Collecitons.Add(c);
            await context.SaveChangesAsync();
            return CreatedAtAction("AddCollection", "api/Collection", collection);

        }
        [HttpPut("{CollectionId}/{SongId}/{isAdd}")]
        public async Task<ActionResult<int>> UpdateCollection([FromRoute]int CollectionId,[FromRoute]int SongId,[FromRoute] int isAdd = 1)
        {
            var collection = await context.Collections
                .Where(s => s.CollectionId == CollectionId)
                .Include(t => t.accounts).FirstOrDefaultAsync();
            var song = await context.Songs
                .Where(s => s.SongId == SongId)
                .Include(t => t.collections).FirstOrDefaultAsync();
            if (collection == null|| song == null)
            {
                return NotFound();
            }
            if(isAdd == 1)
            {
                collection.songs.Add(song);
                song.collections.Add(collection);
            }else
            {
                if (!collection.songs.Remove(song) || !song.collections.Remove(collection))
                    return NotFound();
            }
            

            var res = context.Collections.Update(collection);
            var res2 = context.Songs.Update(song);

            if (res.State == EntityState.Modified || res2.State == EntityState.Modified)
                return await context.SaveChangesAsync();
            else if (res.State == EntityState.Unchanged || res2.State == EntityState.Unchanged)
                return BadRequest();
            return NoContent();
        }
        
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteCollection([FromRoute]int id)
        {
            var deleteCollection = await context.Collections.FindAsync(id);
            var res = context.Collections.Remove(deleteCollection);
            if (res.State == EntityState.Deleted)
                return await context.SaveChangesAsync();
            else if (res.State == EntityState.Detached)
                return BadRequest();
            return NotFound();
        }
    }
}