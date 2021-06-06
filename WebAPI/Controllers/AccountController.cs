using Microsoft.AspNetCore.Mvc;
using MyApp.Shared.Models;
using MyApp.WebAPI.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MyApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        MyDbContext context;
        public AccountController(MyDbContext _context)
        {
            context = _context;
        }

        
        //[HttpGet("{id}")]
        //public async Task<ActionResult<Account>> GetAccountById(int id)
        //{
        //    var query = from c in context.Accounts
        //                where c.AccountId == id
        //                select c
        //              ;
        //    var res = await query.FirstOrDefaultAsync();
        //    if (res == null)
        //    {
        //        return NotFound();
        //    }
        //    return res;
        //}
        //[HttpGet("{name}")]
        //public async Task<string> VertifyAccount([FromRoute] string name)
        //{
        //    var query = from a in context.Accounts
        //                where a.Name == name
        //                select a
        //                ;
        //    var res = await query.FirstOrDefaultAsync();
        //    if (res == null)
        //    {
        //        return "VertifyFail";
        //    }
        //    return "VertifyPass";
        //}
        [HttpGet("{AccountName}")]
        public async Task<ActionResult<Account>> GetAccountByName([FromRoute] string AccountName)
        {
            var query = from a in context.Accounts
                        where a.Name == AccountName
                        select a
                        ;
            var res = await query.FirstOrDefaultAsync();
            if (res == null)
            {
                return NotFound();
            }
            return res;
        }
        [HttpPost]
        public async Task<IActionResult> AddAccount([FromBody]string name)
        {
            var account = new Account { Name = name };
            var query = from a in context.Accounts
                        where a.Name == name
                        select a
                        ;
            if (query.Any())
                return BadRequest();
            var res = context.Accounts.Add(account);
            await context.SaveChangesAsync();
            return CreatedAtAction("AddAccount", "api/Account", account);

        }
        [HttpPut("{AccountId}/{CollectionId}/{isAdd}")]
        public async Task<ActionResult<int>> UpdateAccount([FromRoute]int AccountId,[FromRoute] int CollectionId,[FromRoute] int isAdd)
        {
            var account = await context.Accounts.FindAsync(AccountId);
            var collection = await context.Collections.FindAsync(CollectionId);
            if (account == null || collection == null)
                return NotFound();

            if (account.Collecitons == null)
                account.Collecitons = new List<Collection>();
            if (collection.accounts == null)
                collection.accounts = new List<Account>();
            if (isAdd == 1)
            {
                account.Collecitons.Add(collection);
                collection.accounts.Add(account);
            }
            else if(isAdd==0)
            {
                if(account.Collecitons.Remove(collection) || collection.accounts.Remove(account) )
                    return NotFound();
            }
                

            var res = context.Accounts.Update(account);
            var res2 = context.Collections.Update(collection);

            if (res.State == EntityState.Modified || res2.State == EntityState.Modified)
                return await context.SaveChangesAsync();
            else if (res.State == EntityState.Unchanged|| res2.State == EntityState.Unchanged)
                return BadRequest();
            return BadRequest();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> DeleteAccount([FromRoute]int id)
        {
            var account = await context.Accounts.FindAsync(id);
            var res = context.Accounts.Remove(account);
            if (res.State == EntityState.Deleted)
                return await context.SaveChangesAsync();
            else if (res.State == EntityState.Detached)
                return BadRequest();
            return NotFound();
        }
    }
}