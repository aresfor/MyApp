using MyApp.Global;
using MyApp.Shared.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
namespace MyApp.Services
{
    class AccountService
    {
        public AccountService()
        {
            
        }
        public async static Task<bool> UpdateAccount(int AccountId,int CollectionId,int isAdd = 1)
        {
            //var json = JsonConvert.SerializeObject(new List<int> {CollectionId, isAdd });
            //var content = new StringContent(json,Encoding.UTF8,"application/json");
            var res = await Client.client.PutAsync("api/Account/"+AccountId+"/"+CollectionId+"/"+isAdd, null);
            if (!res.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }

        //public async static Task GetAccountById(int id)
        //{
        //    //var content = new StringContent(id.ToString());
        //    var res = await Client.client.GetAsync("api/Account/"+id.ToString());
        //     if (!res.IsSuccessStatusCode)
        //    {

        //    }
        //}
        //public async static Task<bool> VerifyAccount(string name)
        //{
        //    var res = await Client.client.GetStringAsync("api/Account/"+name );
        //    //var res = JsonConvert.DeserializeObject<String>(json);
        //    if (res.Equals("VertifyPass"))
        //        return true;
        //    else if (res.Equals("VertifyFail"))
        //        return false;
        //    return false;
        //}
        public async static Task<Account> GetAccount(string AccountName)
        {
            var json = await Client.client.GetStringAsync("api/Account/" + AccountName);

            return JsonConvert.DeserializeObject<Account>(json);
        }
        public async static Task<bool> Register(string name)
        {
            var json = JsonConvert.SerializeObject(name);
            //var content = new StringContent(json,Encoding.UTF8,"application/json");
            var content = new StringContent(json,Encoding.UTF8,"application/json");
            var res = await Client.client.PostAsync("api/Account",content);
            if(!res.IsSuccessStatusCode)
            {
                return false;
            }
            return true;
        }
    }
}
