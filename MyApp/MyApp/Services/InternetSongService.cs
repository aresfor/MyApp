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
namespace MyApp.Services
{
    public class datastore
    {
        //本地Android调试所用的本地证书
        public void SetCertificate(ref HttpClient client)
        {
        var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback =
   (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
        }
        
    }
    class InternetSongService
    {
        //Android localDebug url:"https://10.0.2.2:5001"
        static string BaseUrl = "https://1.117.156.191:5001";

        static HttpClient client;
        
        static string imageURL = "https://vignette4.wikia.nocookie.net/vocalopedia/images/b/b5/Original.jpg";

        static InternetSongService()
        {
            //本地证书
            //datastore d = new datastore();
            //d.SetCertificate(ref client);
            //client.BaseAddress = new Uri(BaseUrl);


            client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };


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
        static public async Task<IEnumerable<Song>> GetSong() =>
            await GetAsync<IEnumerable<Song>>("api/InterNetSong", "getSongs");
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
                    json = await client.GetStringAsync(url);

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
            var response = await client.PostAsync("api/InterNetSong",content);
            if(!response.IsSuccessStatusCode)
            {
                //执行无法提交之后的代码
            }
        }
        static public async Task UpdateSong(Song song)
        {
            var json = JsonConvert.SerializeObject(song);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync("api/InterNetSong", content);
            if(!response.IsSuccessStatusCode)
            {
                //执行无法更新之后的代码

            }
        }
        static public async Task DeleteSong(int id)
        {
            var response = await client.DeleteAsync($"api/InterNetSong/{id}");
            if(!response.IsSuccessStatusCode)
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
