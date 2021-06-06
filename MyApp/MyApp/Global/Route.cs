using MyApp.Shared.Models;
using MyApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyApp.Global
{
    public static class Route
    {
        public static async Task GoToPage(string route)
        {
            await Shell.Current.GoToAsync(route);
        }
        public static async Task GoToSongDetailPage(Song song)
            => await GoToPage($"/{nameof(DetailPage)}?song = {song}");
    }
}
