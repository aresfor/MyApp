using MyApp.Global;
using MyApp.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.Services
{
    public  class CollectionService
    {
        public CollectionService()
        {

        }

        public static async Task<IEnumerable<Collection>> GetCollection()
        {
           var json =  await Client.client.GetStringAsync("api/Collection");
            //string json = string.Empty;
            //while (!res.IsCompleted)
            //{
            //    json = res.Result;

            //}
            var collections = JsonConvert.DeserializeObject<IEnumerable<Collection>>(json);
            return collections;
        }
        public static async Task<IEnumerable<Collection>> GetCollectionByAccountId(int AccountId)
        {
            var json = await Client.client.GetStringAsync("api/Collection/" + AccountId.ToString());
            var collection = JsonConvert.DeserializeObject<IEnumerable<Collection>>(json);
            return collection;
        }
        public static async Task AddCollection(string AccountName, string CollectionName)
        {
            //var json = JsonConvert.SerializeObject(new List<String> { AccountName, CollectionName });
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await Client.client.PostAsync("api/Collection" + AccountName+"/"+CollectionName, null);
        }
        public static async Task AddSongToCollection(int CollectionId,int SongId)
        {
            //var json = JsonConvert.SerializeObject(new List<int> { CollectionId, SongId });
            //var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = await Client.client.PutAsync("api/Collection/"+CollectionId.ToString()+"/"+SongId.ToString(),null);
        }
        public static async Task DeleteCollection(int CollectionId)
        {
            var res = await Client.client.DeleteAsync("api/Collection/" + CollectionId.ToString());
        }
    }
}
