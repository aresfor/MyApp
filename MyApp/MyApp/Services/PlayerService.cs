using MyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace MyApp.Services
{
    static class PlayerService
    {
        static string PreName = null;
        public static void InitPlayer(string name)
        {
            if (PreName == name)
                return;
            PreName = name;
            var source = DependencyService.Get<IAssets>().GetMusicPath(name);
            DependencyService.Get<IPlayer>().Init(source);
        }
        public static void PlayOrPause()
        {
            DependencyService.Get<IPlayer>().PlayOrPause();
        }
    }
}
