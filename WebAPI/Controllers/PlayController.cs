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
        MyDbContext context;
        public PlayController(MyDbContext context)
        {
            this.context = context;
        }
        [HttpGet("{SongName}")]
        public FileStream GetStreamMusic([FromRoute]string SongName)
        {
            var MusicPath = Global.Path.MusicPath;
            var path = Directory.GetFiles(MusicPath, SongName);
            //var fileInfo = new FileInfo(path[0]);
            return StreamService.GetReadFileStream(path[0]);
        }

        // POST webroot/xxx/xx/endpoint?filename=xx.mp3 & xx.mp3

        [HttpPost("{filename}")]
        public async Task PostUploadSong(Stream stream, [FromQuery] string filename)
        {
            var file = new FileInfo(Global.Path.MusicPath + filename);
            if (!file.Exists)
            {
                file.Create();
            }
            var fs = file.OpenWrite();
            await stream.CopyToAsync(fs);
            //同时在客户端还要调用向数据库添加歌曲的命令
        }
        

        [HttpPut]
        public async Task LoadServiceLocalSongToDataBase()
        {
            List<Song> songs = new List<Song>();
            var MusicPath = Global.Path.MusicPath;
            var path = Directory.GetFiles(MusicPath);
            
            //FileInfo f = null;
            foreach (var p in path)
            {
                TagLib.File tagFile = TagLib.File.Create(p);
                //f = new FileInfo(p);
                var bytes = tagFile.Tag.Pictures[0].Data.Data;
                string imagePath = Global.Path.ImagePath + tagFile.Name;
                System.IO.File.WriteAllBytes(imagePath, bytes);
                songs.Add(new Song
                {
                    Name = tagFile.Name,
                    DisplayName = tagFile.Tag.Title,
                    Singer = tagFile.Tag.FirstPerformer,
                    Length = tagFile.Length.ToString(),
                    Image = "IA.jpg"
                });
            }
            context.Songs.UpdateRange(songs);
            await context.SaveChangesAsync();
        }
    }
}