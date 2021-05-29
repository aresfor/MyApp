using Microsoft.AspNetCore.Mvc;
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
    public class SongController : ControllerBase
    {
        static string imageURL = "https://vignette4.wikia.nocookie.net/vocalopedia/images/b/b5/Original.jpg";
        public static List<Song> songs { get; } = new List<Song>();
       

        public SongController()
        {
            List<Song> list = new List<Song> { new Song{Id = 1,Name = "All Along with you", Singer = "EGOIST", Length = "3:44",Image = imageURL },
            new Song { Id = 2, Name = "All Along with you", Singer = "EGOIST", Length = "3:44", Image = imageURL },
                new Song { Id = 2, Name = "All Along with you", Singer = "EGOIST", Length = "3:44", Image = imageURL } };

            songs.AddRange(list);
        }
        [HttpGet]
        public IEnumerable<Song> GetSong()
        {
            //songs.Add(new Song("All Along with you", "EGOIST", "3:44", imageURL));
            return songs;
        }
        [HttpGet("{id}")]
        public Song GetSong(int id)
        {
            return songs.FirstOrDefault(s => s.Id == id);
        }
        [HttpPost]
        public void AddSong([FromBody] Song value)
        {
            songs.Add(value);
        }
        [HttpPut("{id}")]
        public void UpdateSong(int id, [FromBody]Song value)
        {
            var song = songs.FirstOrDefault(s => s.Id == id);
            if (song == null)
                return;

            song = value;
        }
        [HttpDelete("{id}")]
        public void DeleteSong(int id)
        {
            var song = songs.FirstOrDefault(s => s.Id == id);
            if (song == null)
                return;

            songs.Remove(song);
        }
             
    }
}