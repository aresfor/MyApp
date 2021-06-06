using MyApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MediaManager;
using System.Threading.Tasks;
using System.IO;
using MyApp.Global;

namespace MyApp.Services
{
    static class PlayerService
    {
        static string PreName = null;
        public static double Length = 0;
        public static async Task SetSong(string name)
        {
            if (PreName == name)
                return;
            PreName = name;
            var path = DependencyService.Get<IAssets>().GetMusicPath();
            //匹配！
            string[] source = Directory.GetFiles(path, name + ".*");
            
            if (source.Length !=0)
            {
                try
                {
                    await CrossMediaManager.Current.Play(source[0]);
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                try
                {
                    await CrossMediaManager.Current.Play(await Client.client.GetStreamAsync("api/Play/" + name), name);
                    //可以获取完整的音乐信息
                    //MediaManager.Library.MediaItem a = new MediaManager.Library.MediaItem();

                }catch(Exception ex)
                {

                }
            }
        }
        public async static Task Play()
        {
            await CrossMediaManager.Current.Play();
                //var route = DependencyService.Get<IAssets>().GetMusicPath(source);
                //await CrossMediaManager.Current.Play(route);
                //Length = CrossMediaManager.Current.Queue.Current.Duration.TotalSeconds;
            
            //DependencyService.Get<IPlayer>().Play();
        }
        public static void Pause()
        {
            CrossMediaManager.Current.Pause();
           // DependencyService.Get<IPlayer>().Pause();
        }
        public static  bool isPlaying()
        {
            return CrossMediaManager.Current.IsPlaying();
            //return DependencyService.Get<IPlayer>().isPlaying();
        }
        public async static Task SeekTo(double sec)
        {
            int min = (int)sec / 60;
            int s = (int)sec % 60;
            TimeSpan ts = new TimeSpan(0, min, s);
            await CrossMediaManager.Current.SeekTo(ts);
            //DependencyService.Get<IPlayer>().SeekTo(msec);
        }
    }
}
