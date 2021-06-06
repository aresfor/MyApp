using Microsoft.AspNetCore.Mvc;
using MyApp.Shared.Models;
using MyApp.WebAPI.Context;
using MyApp.WebAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyApp.WebAPI.Global;
namespace MyApp.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayController : ControllerBase
    {
        public PlayController()
        {

        }
        [HttpGet("{SongName}")]
        public FileStream GetStreamMusic([FromRoute]string SongName)
        {
            var MusicPath = Global.Path.MusicPath;
            var path = Directory.GetFiles(MusicPath, SongName + ".*");
            //var fileInfo = new FileInfo(path[0]);
            return StreamService.GetReadFileStream(path[0]);
        }

        // POST webroot/xxx/xx/endpoint?filename=xx.mp3 & xx.mp3

        [HttpPost("{filename}")]
        public async Task PostUploadSong(Stream stream, [FromQuery] string filename)
        {
            var file = new FileInfo($"./songs/upload/{filename}");
            if (!file.Exists)
            {
                file.Create();
            }
            var fs = file.OpenWrite();
           await stream.CopyToAsync(fs);

        }

    }
}