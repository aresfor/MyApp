using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
using Xamarin.Essentials;
using System.Threading.Tasks;
using MyApp.Shared.Models;


namespace MyApp.Services
{
    class SongService
    {
        static SQLiteAsyncConnection db;
        static async Task Init()
        {
            if (db != null)
                return;
            var databasePath = Path.Combine(FileSystem.AppDataDirectory, "SongData.db");
            db = new SQLiteAsyncConnection(databasePath);
            var result = await db.CreateTableAsync<Song>();
        }
        static public async Task AddSong(string name,string singer,string length)
        {
            await Init();
            string imageURL = "https://www.gematsu.com/wp-content/uploads/2014/01/IA-PSV-Game-Init.jpg";

            var song = new Song
            {
                Name = name,
                Singer = singer,
                Length = length,
                Image = imageURL
            };
            var id = await db.InsertAsync(song);
        }
        static public async Task UpdateSong(string name,string singer,string length)
        {
            await Init();
            //db.upda
            
        }
        static public async Task DeleteSong(int id)
        {
            await Init();
            await db.DeleteAsync<Song>(id);
        }
        static public async Task<IEnumerable<Song>> GetSong()
        {
            await Init();

            var song = await db.Table<Song>().ToListAsync();
            return song;
        }
        static public async Task<Song> GetSongById(int id)
        {
            await Init();
            var song = await db.FindAsync<Song>(id);
            return song;
        }
    }
}
