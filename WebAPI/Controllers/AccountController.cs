using Microsoft.AspNetCore.Mvc;
using MyApp.Shared.Models;
using MyApp.WebAPI.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
        //[HttpGet("{account}")]
        //public async Task<ActionResult<bool>> Vertify(Account account)
        //{
            //var query = from a in context.Accounts.AsNoTracking()
            //            where a.Id == account.Id
            //            select a
            //            ;
            //var res =await query.FirstOrDefaultAsync();
            //if (res == null)
            //    return NotFound();
            //if (res.passwd == account.passwd)
            //    return true;
            //else
            //    return false;
        //}
    }
}