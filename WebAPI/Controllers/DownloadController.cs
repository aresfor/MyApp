using Microsoft.AspNetCore.Mvc;
using MyApp.WebAPI.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DownloadController:ControllerBase
    {
        MyDbContext context;
        public DownloadController(MyDbContext _context)
        {
            context = _context;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Download(int id)
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
            var fileStream = Services.DownloadService.GetReadFileStream("../MusicStore/" + res.Name);
            return File(fileStream, "application/octet-stream");
        }
    }
}