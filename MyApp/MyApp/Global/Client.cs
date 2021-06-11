using MyApp.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace MyApp.Global
{
    public static class Client
    {
        //Android localDebug url:"https://10.0.2.2:5001",WebAPi:"https://cqupt426.top:5001"
        static string BaseUrl = "https://cqupt426.top:5001";

        public static HttpClient client;
       // public class datastore
       // {
       //     //本地Android调试所用的本地证书
       //     public void SetCertificate(ref HttpClient client)
       //     {
       //         var httpClientHandler = new HttpClientHandler();
       //         httpClientHandler.ServerCertificateCustomValidationCallback =
       //(message, cert, chain, errors) => { return true; };

       //         client = new HttpClient(httpClientHandler);
       //     }

       // }
        public static void Init()
        {
            //var httpClientHandler = new HttpClientHandler();
            //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            //client = new HttpClient(httpClientHandler);
            ////本地证书
            //client.BaseAddress = new Uri(BaseUrl);



            client = new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            };
            client.Timeout = TimeSpan.FromMinutes(1);
        }
    }
}
