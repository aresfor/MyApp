using Android.Content;
using MyApp.Global;
using MyApp.Interfaces;
using MyApp.Shared.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyApp.Services
{
    public static class LocalSongService
    {
        public  static IEnumerable<Song> GetSong()
        {
            return  DependencyService.Get<IAssets>().GetLocalSongs();
        }
    }
}
