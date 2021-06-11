using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
using Xamarin.Essentials;
using System.Threading.Tasks;
using MyApp.Shared.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Diagnostics;
using MonkeyCache.FileStore;
using MyApp.Global;

namespace MyApp.Services
{
    
    class InternetSongService
    {
        
        static string imageURL = "https://vignette4.wikia.nocookie.net/vocalopedia/images/b/b5/Original.jpg";

        public InternetSongService()
        {
            

        }

        //static public async Task<IEnumerable<Song>> GetSong()
        //{
        //    var json = await client.GetStringAsync("api/Song");
        //    var songs = JsonConvert.DeserializeObject<IEnumerable<Song>>(json);
        //    return songs;
        //}
        //static public async Task<Song> GetSongById(int id)
        //{
        //    return await client.GetAsync()
        //}
        static public async Task<IEnumerable<Song>> GetSongByCollectionId(int CollectionId) =>
            await GetAsync<IEnumerable<Song>>("api/InternetSong/" + CollectionId.ToString(), "getSongsByCollectionId", 1, true);
        static public async Task<IEnumerable<Song>> GetSong() =>
            await GetAsync<IEnumerable<Song>>("api/InternetSong", "getSongs",1,true);
        static async Task<T> GetAsync<T>(string url, string key, int mins = 1, bool forceRefresh = false)
        {
            var json = string.Empty;

            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                json = Barrel.Current.Get<string>(key);
            else if (!forceRefresh && !Barrel.Current.IsExpired(key))
                json = Barrel.Current.Get<string>(key);

            try
            {
                if (string.IsNullOrWhiteSpace(json))
                {
                    json = await Client.client.GetStringAsync(url);

                    Barrel.Current.Add(key, json, TimeSpan.FromMinutes(mins));
                }
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (Exception ex)
            {
                //无法读取，报错
                Debug.WriteLine($"Unable to get information from server {ex}");
                throw ex;
            }
        }

        static public async Task AddSong(string name, string singer, string length)
        {

            var song = new Song
            {
                Name = name,
                Singer = singer,
                Length = length,
                Image = imageURL
            };
            var json = JsonConvert.SerializeObject(song);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.client.PostAsync("api/InternetSong",content);
            if(!response.IsSuccessStatusCode)
            {
                //执行无法提交之后的代码
            }
        }
        static public async Task UpdateSong(Song song)
        {
            var json = JsonConvert.SerializeObject(song);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await Client.client.PutAsync("api/InterNetSong", content);
            if(!response.IsSuccessStatusCode)
            {
                //执行无法更新之后的代码

            }
        }
        static public async Task DeleteSong(int id)
        {
            var response = await Client.client.DeleteAsync($"api/InterNetSong/{id}");
            if(!response.IsSuccessStatusCode)
            {
                //执行无法提交之后的代码

            }
        }
        static public async Task LoadServerLocalSong()
        {
            var response = await Client.client.PutAsync("api/Play", null);
            if (!response.IsSuccessStatusCode)
            {
                //执行无法提交之后的代码

            }
        }
        //static public async Task<Song> GetSongById(int id)
        //{
        //    //var song = await db.FindAsync<Song>(id);
        //    //return song;
        //}
    }
}
